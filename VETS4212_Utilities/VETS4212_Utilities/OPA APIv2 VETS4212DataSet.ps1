############################################################################
## This will retrieve VETS-4212 report records from OPA APIv2
##
## Parameters
## $apiKey - Created on developer.dol.gov
## $dateCompareColumn - Column used for date comparison of records to return
## $startDate - Staring date for ending period - 2017-07-01 
## $endDate - Ending date for ending period - 2017-12-31
## $columnNames - listing of columns to search
## $matchRegEx - Match regular expression to search column
## $cvsFilePath - Where to save the matching records
## $restAddress should be either data.dol.gov or quarry.dol.gov
## $requestedRecordLimit - The maximum number of records to return each loop
## 
############################################################################
[CmdLetBinding()]
Param(
    [Parameter(Mandatory=$false, Position=0)]
    [String] $apiKey = "59656247-2de2-4fc9-bdc7-7cd63de01fcc",
    [Parameter(Mandatory=$false, Position=1)]
    [String] $dateCompareColumn = "EndingPeriod",
    [Parameter(Mandatory=$false, Position=2)]
    [String] $startDate = "2015-07-01",
    [Parameter(Mandatory=$false, Position=3)]
    [String] $endDate = "2015-12-31",
    [Parameter(Mandatory=$false, Position=4)]
    [String[]] $columnNames = @("CoName","HlName"),
    [Parameter(Mandatory=$false, Position=5)]
    [String[]] $matchRegEx = @("(Mayer Brown|Simpson Thacher and Bartlett)","(Mayer Brown|Simpson Thacher and Bartlett)"),
    [Parameter(Mandatory=$false, Position=6)]
    [String] $cvsFilePath = "F:\VETS-4212\matchedRecords.csv",
    [Parameter(Mandatory=$false, Position=7)]
    [String] $restAddress = "quarry.dol.gov",
    [Parameter(Mandatory=$false, Position=8)]
    [Int] $requestedRecordLimit = 200
)

# Load required assembly for Json parsing
[System.Reflection.Assembly]::LoadWithPartialName("System.Web.Extensions")

# Local variables
$offset = 0;
$headerWriten = $false;

# Create File handle to write matching files
$file = [System.IO.File]::Open($cvsFilePath, [System.IO.FileMode]::Create);
$tw = [System.IO.StreamWriter]($file);

# once the file is opened we need to make sure we dispose
try 
{
    # keep getting records till we get error 400
    while ($offset -ne -1)
    {
        # Set the URI of the REST service
        $URI = [System.Uri]$("https://$restAddress/get/vets4212dataset/limit/$requestedRecordLimit/offset/$offset/date_column/$dateCompareColumn/start_date/$startDate/end_date/$endDate");

        # Create a new web request
        $WebRequest = [System.Net.HttpWebRequest]::Create($URI);

        # Add the token header
        $webRequest.Headers.Add("X-API-KEY", $apiKey);

        # Set current proxy information
        $webRequest.Proxy = [System.Net.WebProxy]::GetDefaultProxy();

        # Set the current security protocol to latest version
        [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.SecurityProtocolType]::Tls12 -bor [System.Net.ServicePointManager]::SecurityProtocol


        # Set the HTTP method
        $WebRequest.Method = 'GET';

        try 
        {
            # Get the response stream
            $response = $WebRequest.GetResponse();

            # Create a stream reader to get all the data
            $reader = [System.IO.StreamReader]($response.GetResponseStream());

            # Convert the Json data to native PowerShell object
            $ser = New-Object System.Web.Script.Serialization.JavaScriptSerializer
            $reports = $ser.DeserializeObject($reader.ReadToEnd());

            # Write the header if hasn't been writen
            if (($headerWriten -ne $true) -and ($reports.Count -gt 0)) {
                [System.IO.TextWriter]($tw).WriteLine([System.String]::Join(",", $reports[0].Keys));
                $headerWriten = $true;
            }

            # Go through each report and determine if it matches
            $reports | % { 
                for ($i = 0; $i -lt $columnNames.Count; $i++) {
                    if ($_[$columnNames[$i]] -imatch $matchRegEx[$i]) {
                        [System.IO.TextWriter]($tw).WriteLine([System.String]::Join(",", $_.Values));
                        break;
                    }
                }
            }

            # close response
            $reader.Close();
            $response.Close();

            # increment offset
            $offset = $offset + $reports.Count;
            Write-Host "Completed processing $offset records..."

            # if we received less than the request amount we are done
            if ($reports.Count -lt $requestedRecordLimit) { $offset = -1; }
        }
        catch
        {
            Write-Host "Last Message: $_"
            Write-Host "Finished Getting Records";
            $offset = -1;
        }
    }
}
finally
{
    # close file
    if ($tw -ne $null) { 
        $tw.Flush();
        $tw.Dispose();
    }
    if ($file -ne $null) { $file.Dispose(); }
}

# Completed Script
Write-Host "Completed executing script";
