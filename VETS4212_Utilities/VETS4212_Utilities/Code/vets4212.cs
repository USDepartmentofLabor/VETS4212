using System;
using System.IO;
using System.Net;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace gov.dol.vets.utilities
{
    // State objects used to pass to thread queue
    public class vets4212StateObject
    {
        string _dataFilename = null;
        string _hlDataFilename = null;
        string _username = null;
        string _password = null;
        bool _internalAccess = false;
        string _webAddress = null;
        string _companyInformationAddress = null;

        public vets4212StateObject(string DataFilename, string HiringLocationDataFilename, string Username, string Password, string WebAddress, string CompanyInformationAddress, bool InternalAccess)
        {
            this._dataFilename = DataFilename;
            this._hlDataFilename = HiringLocationDataFilename;
            this._username = Username;
            this._password = Password;
            this._internalAccess = InternalAccess;
            this._webAddress = WebAddress;
            this._companyInformationAddress = CompanyInformationAddress;
        }

        public string DataFilename { get { return this._dataFilename; } }
        public string HiringLocationDataFilename { get { return this._hlDataFilename; } }
        public string Username { get { return this._username; } }
        public string Password { get { return this._password; } }
        public bool InternalAccess { get { return this._internalAccess; } }
        public string WebAddress { get { return this._webAddress; } }
        public string CompanyInformationAddress { get { return this._companyInformationAddress; } }
    }
    public class vets4212ReportStats
    {
        public class stateReports
        {
            // local variables
            string _state = null;
            int _sReports = 0;
            int _mhqReports = 0;
            int _mhlReports = 0;
            int _mscReports = 0;

            public stateReports() { }

            public string State
            {
                get { return this._state; }
                set { this._state = value; }
            }
            public int SReports
            {
                get { return this._sReports; }
                set { this._sReports = value; }
            }
            public int MHQReports
            {
                get { return this._mhqReports; }
                set { this._mhqReports = value; }
            }
            public int MHLReports
            {
                get { return this._mhlReports; }
                set { this._mhlReports = value; }
            }
            public int MSCReports
            {
                get { return this._mscReports; }
                set { this._mscReports = value; }
            }
        }

        // local variables
        int _totalReports = 0;
        Dictionary<string, stateReports> _reports = new Dictionary<string, stateReports>();

        public vets4212ReportStats() { }

        // properties of reports
        public int TotalReports
        {
            get { return this._totalReports; }
            set { this._totalReports = value; }
        }
        public Dictionary<string, stateReports> Reports
        {
            get { return this._reports; }
            set { this._reports = value; }
        }
    }
    public class pdfStateObject
    {
        private string _username = null;
        private string _password = null;
        private string _filename = null;
        private string _webAddress = null;
        private string _pdfAddress = null;
        private string _reportIDUrl = null;
        private string _reportSearchUrl = null;
        private string _companyName = null;
        private string _filingCycle = null;
        private string _reportType = null;
        private string _companyNumber = null;
        private bool _internalAccess = false;

        public pdfStateObject(string reportIDUrl, string Username, string Password, string Filename, string webAddress,
            string pdfAddress, string reportSearchAddress, string companyName, string filingCycle, string reportType)
        {
            this._username = Username;
            this._password = Password;
            this._filename = Filename;
            this._webAddress = webAddress;
            this._pdfAddress = pdfAddress;
            this._reportIDUrl = reportIDUrl;
            this._reportSearchUrl = reportSearchAddress;
            this._companyName = companyName;
            this._filingCycle = filingCycle;
            this._reportType = reportType;
        }
        public pdfStateObject(string reportIDUrl, string Username, string Password, string Filename, string webAddress,
            string pdfAddress, string reportSearchAddress, string companyName, string filingCycle, string reportType, string CompanyNumber, bool InternalAccess)
        {
            this._username = Username;
            this._password = Password;
            this._filename = Filename;
            this._webAddress = webAddress;
            this._pdfAddress = pdfAddress;
            this._reportIDUrl = reportIDUrl;
            this._reportSearchUrl = reportSearchAddress;
            this._companyName = companyName;
            this._filingCycle = filingCycle;
            this._reportType = reportType;
            this._internalAccess = InternalAccess;
            this._companyNumber = CompanyNumber;
        }

        public string Username { get { return this._username; } }
        public string Password { get { return this._password; } }
        public string Filename { get { return this._filename; } }
        public string webAddress { get { return this._webAddress; } }
        public string pdfAddress { get { return this._pdfAddress; } }
        public string reportIDUrl { get { return this._reportIDUrl; } }
        public string reportSearchAddress { get { return this._reportSearchUrl; } }
        public string CompanyName { get { return this._companyName; } }
        public string FilingCycle { get { return this._filingCycle; } }
        public string ReportType { get { return this._reportType; } }
        public bool InternalAccess { get { return this._internalAccess; } }
        public string CompanyNumber { get { return this._companyNumber; } }
    }
    public class dataDotGovStateObject
    {
        private string _dataDotGovAddress = null;
        private string _companyName = null;
        private string _filingCycle = null;
        private string _dataFilePath = null;
        private string _reportType = null;
        private string _faics = null;

        public dataDotGovStateObject(string DataDotGovAddress, string CompanyName, string ReportType, string FilingCycle, string NAICS, string DataFilePath)
        {
            this._dataDotGovAddress = DataDotGovAddress;
            this._companyName = CompanyName;
            this._filingCycle = FilingCycle;
            this._dataFilePath = DataFilePath;
            this._reportType = ReportType;
            this._faics = NAICS;
        }

        public string DataDotGovAddress { get { return this._dataDotGovAddress; } }
        public string CompanyName { get { return this._companyName; } }
        public string FilingCycle { get { return this._filingCycle; } }
        public string DataFilePath { get { return this._dataFilePath; } }
        public string ReportType { get { return this._reportType; } }
        public string NAICS { get { return this._faics; } }
    }

    // company information
    public class CompanyInformation
    {
        private string _companyName = string.Empty;
        private string _companyNumber = string.Empty;
        private string _ein = string.Empty;
        private string _duns = string.Empty;
        private string _naics = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _emailAddress = string.Empty;
        private string _address = string.Empty;
        private string _county = string.Empty;
        private string _city = string.Empty;
        private string _state = string.Empty;
        private string _zipcode = string.Empty;

        // creators
        public CompanyInformation(string CompanyName, string CompanyNumber, string EIN, string DUNS, string NAICS, string Firstname,
            string Lastname, string Phone, string Email, string Address, string County, string City, string State, string Zipcode)
        {
            this._companyName = CompanyName;
            this._companyNumber = CompanyNumber;
            this._ein = EIN;
            this._duns = DUNS;
            this._naics = NAICS;
            this._firstName = Firstname;
            this._lastName = Lastname;
            this._phoneNumber = Phone;
            this._emailAddress = Email;
            this._address = Address;
            this._county = County;
            this._city = City;
            this._state = State;
            this._zipcode = Zipcode;
        }
        public CompanyInformation()
        {
            // nothing to do
        }

        // properties
        public string CompanyName
        {
            get { return this._companyName; }
            set { this._companyName = value; }
        }
        public string CompanyNumber
        {
            get { return this._companyNumber; }
            set { this._companyNumber = value; }
        }
        public string EIN
        {
            get { return this._ein; }
            set { this._ein = value; }
        }
        public string DUNS
        {
            get { return this._duns; }
            set { this._duns = value; }
        }
        public string NAICS
        {
            get { return this._naics; }
            set { this._naics = value; }
        }
        public string Firstname
        {
            get { return this._firstName; }
            set { this._firstName = value; }
        }
        public string Lastname
        {
            get { return this._lastName; }
            set { this._lastName = value; }
        }
        public string Phone
        {
            get { return this._phoneNumber; }
            set { this._phoneNumber = value; }
        }
        public string Email
        {
            get { return this._emailAddress; }
            set { this._emailAddress = value; }
        }
        public string Address
        {
            get { return this._address; }
            set { this._address = value; }
        }
        public string County
        {
            get { return this._county; }
            set { this._county = value; }
        }
        public string City
        {
            get { return this._city; }
            set { this._city = value; }
        }
        public string State
        {
            get { return this._state; }
            set { this._state = value; }
        }
        public string Zipcode
        {
            get { return this._zipcode; }
            set { this._zipcode = value; }
        }
    }

    // event arguments for message event
    public class MessageEventArgs : EventArgs
    {
        private string _message = null;

        public MessageEventArgs(string Message)
            : base()
        {
            this._message = Message;
        }

        public string Message
        {
            get { return (this._message); }
        }
    }
    public class EnvironmentEventArgs : EventArgs
    {
        long _memoryUsed = 0;
        long _pageCount = 0;
        long _fileSize = 0;

        public EnvironmentEventArgs(long MemoryUsed, long PageCount, long FileSize)
            : base()
        {
            this._memoryUsed = MemoryUsed;
            this._pageCount = PageCount;
            this._fileSize = FileSize;
        }

        public long MemoryUsed { get { return this._memoryUsed; } }
        public long PageCount { get { return this._pageCount; } }
        public long FileSize { get { return this._fileSize; } }
    }
    public class PDFEventArgs : EventArgs
    {
        private int _numberOfReports = 0;
        private string _message = string.Empty;

        public PDFEventArgs(int NumberOfReports, string Message)
        {
            this._numberOfReports = NumberOfReports;
            this._message = Message;
        }

        public string Message { get { return this._message; } }
        public int NumberOfReports { get { return this._numberOfReports; } }
    }
    public class EvaluateFlatFileEventArgs : EventArgs
    {
        private string _message = string.Empty;

        public EvaluateFlatFileEventArgs(string Message)
        {
            this._message = Message;
        }

        public string Message { get { return this._message; } }
    }
    public class FixFlatFileEventArgs : EventArgs
    {
        private string _message = string.Empty;

        public FixFlatFileEventArgs(string Message)
        {
            this._message = Message;
        }

        public string Message { get { return this._message; } }
    }
    public class DataDotGovEventArgs : EventArgs
    {
        private string _message = string.Empty;

        public DataDotGovEventArgs(string Message)
        {
            this._message = Message;
        }

        public string Message { get { return this._message; } }
    }

    // class for querying OPA API
    public class opaDataDotGovArgs
    {
        // internal veriables
        string _url = null;
        string _filename = null;
        string _reportType = null;

        public opaDataDotGovArgs(string Url, string Filename, string ReportType)
        {
            this._url = Url;
            this._filename = Filename;
            this._reportType = ReportType;
        }

        public string Url { get { return this._url; } }
        public string Filename { get { return this._filename; } }
        public string ReportType { get { return this._reportType; } }
    }

    // class for generating uinque long strings
    public sealed class Password
    {
        #region Local Variables
        private readonly char[] _Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private readonly char[] _Numbers = "1234567890".ToCharArray();
        private readonly char[] _Symbols = "!@#$%^&*.?".ToCharArray();
        private RNGCryptoServiceProvider _Random = new RNGCryptoServiceProvider();
        private Int32 _MinimumLength = 8;
        private Int32 _MaximumLength = 24;
        private bool _IncludeUpper = true;
        private bool _IncludeLower = true;
        private bool _IncludeNumber = true;
        private bool _IncludeSpecial = true;
        private byte[] _bytes = new byte[4];
        private string[] _CharacterTypes = null;

        //'**************************************************************************
        //'** Enumeration
        //'**************************************************************************
        private enum CharacterType : int
        {
            Uppercase = 0,
            Lowercase = 1,
            Special = 2,
            Number = 3
        };
        #endregion

        #region Constructors
        //'**************************************************************************
        //'** New
        //'**************************************************************************
        public Password()
        {
            // Initialize
        }
        public Password(Int32 MinimumLength, Int32 MaximumLength)
        {
            // Intialize
            this._MinimumLength = MinimumLength;
            this._MaximumLength = MaximumLength;
        }
        public Password(bool IncludeUpper, bool IncludeLower, bool IncludeNumber, bool IncludeSpecial)
        {
            this._IncludeLower = IncludeLower;
            this._IncludeNumber = IncludeNumber;
            this._IncludeSpecial = IncludeSpecial;
            this._IncludeUpper = IncludeUpper;
        }
        public Password(Int32 MinimumLength, Int32 MaximumLength, bool IncludeUpper, bool IncludeLower, bool IncludeNumber, bool IncludeSpecial)
        {
            this._MinimumLength = MinimumLength;
            this._MaximumLength = MaximumLength;
            this._IncludeLower = IncludeLower;
            this._IncludeNumber = IncludeNumber;
            this._IncludeSpecial = IncludeSpecial;
            this._IncludeUpper = IncludeUpper;
        }
        #endregion

        #region Properties
        //'**************************************************************************
        //'** Properties
        //'**************************************************************************
        public Int32 MinimumLength
        {
            get { return (this._MinimumLength); }
            set { this._MinimumLength = value; }
        }
        public Int32 MaximumLength
        {
            get { return (this._MaximumLength); }
            set { this._MaximumLength = value; }
        }
        public bool IncludeUpper
        {
            get { return (this._IncludeUpper); }
            set { this._IncludeUpper = value; }
        }
        public bool IncludeLower
        {
            get { return (this._IncludeLower); }
            set { this._IncludeLower = value; }
        }
        public bool IncludeNumber
        {
            get { return (this._IncludeNumber); }
            set { this._IncludeNumber = value; }
        }
        public bool IncludeSpecial
        {
            get { return (this._IncludeSpecial); }
            set { this._IncludeSpecial = value; }
        }
        #endregion

        //'**************************************************************************
        //'** Wrapper for the Random Number Generator
        //'**************************************************************************
        private Int32 _Next(Int32 Max)
        {
            try
            {
                if (Max <= 0) throw new ArgumentOutOfRangeException("Max");
                _Random.GetBytes(_bytes);
                return (Math.Abs(BitConverter.ToInt32(_bytes, 0) % Max));
            }
            catch (Exception ex)
            {
                throw new Exception("Source: _Next", ex);
            }
        }

        //'***************************************************************************
        //'** Procedures
        //'***************************************************************************
        private string[] getCharacterTypes()
        {
            ArrayList characterTypes = new ArrayList();
            CharacterType currentType;

            foreach (string characterType in Enum.GetNames(typeof(CharacterType)))
            {
                currentType = (CharacterType)(Enum.Parse(typeof(CharacterType), characterType, false));
                bool addType = false;
                switch (currentType)
                {
                    case CharacterType.Lowercase:
                        addType = this._IncludeLower;
                        break;

                    case CharacterType.Number:
                        addType = this._IncludeNumber;
                        break;

                    case CharacterType.Special:
                        addType = this._IncludeSpecial;
                        break;

                    case CharacterType.Uppercase:
                        addType = this._IncludeUpper;
                        break;
                }

                // Add the type if indicated
                if (addType) characterTypes.Add(characterType);
            }

            // return the types
            return (string[])(characterTypes.ToArray(typeof(string)));
        }
        private string getCharacter()
        {
            string characterType = _CharacterTypes[this._Next(_CharacterTypes.Length)];
            CharacterType typeToGet = (CharacterType)(Enum.Parse(typeof(CharacterType), characterType, false));
            switch (typeToGet)
            {
                case CharacterType.Lowercase:
                    return (this._Letters[this._Next(_Letters.Length)]).ToString().ToLower();

                case CharacterType.Uppercase:
                    return (this._Letters[this._Next(_Letters.Length)]).ToString().ToUpper();

                case CharacterType.Number:
                    return (this._Numbers[this._Next(_Numbers.Length)]).ToString();

                case CharacterType.Special:
                    return (this._Symbols[this._Next(_Symbols.Length)]).ToString();
            }

            // return empty result
            return (null);
        }
        public string CreatePassword()
        {
            try
            {
                _CharacterTypes = getCharacterTypes();

                StringBuilder password = new StringBuilder(_MaximumLength);

                // Get a random length for the password
                Int32 currentPasswordLength = this._Next(_MaximumLength);

                // Only allow for passwords greater than or equal to the minimum length
                if (currentPasswordLength < _MinimumLength) currentPasswordLength = _MinimumLength;

                // Generate the password
                for (Int32 i = 0; i < currentPasswordLength; i++)
                    password.Append(getCharacter());

                // return the completed password
                return (password.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Source: Password.CreatePassword", ex);
            }
        }
    }

    // class for website querying
    public class vets4212Website
    {
        string _url = string.Empty;
        string _username = string.Empty;
        string _password = string.Empty;
        bool _internalAccess = false;

        public vets4212Website(string Url, string Username, string Password, bool InternalAccess)
        {
            this._url = Url;
            this._username = Username;
            this._password = Password;
            this._internalAccess = InternalAccess;
        }

        public string Url { get { return this._url; } }
        public string Username { get { return this._username; } }
        public string Password { get { return this._password; } }
        public bool InternalAccess { get { return this._internalAccess; } }
    }

    // class for exception handling
    public class vets4212Exception : Exception
    {
        // local variables

        // handle all initializers
        public vets4212Exception()
            : base()
        {
        }
        public vets4212Exception(string message)
            : base(message)
        {
        }
        public vets4212Exception(string message, Exception exception)
            : base(message, exception)
        {
        }
        public vets4212Exception(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }

    // class used to process and verify VETS-4212 batch files
    public class vets4212
    {
        // constants
        private const long MAX_FILE_SIZE = 82000000L;

        // event delegates
        public delegate void MessageEventHandler(object sender, MessageEventArgs e);
        public delegate void EnvironmentEventHandler(object sender, EnvironmentEventArgs e);
        public delegate void PDFReportsCompletedEventHandler(object sender, PDFEventArgs e);
        public delegate void EvaluateFlatFileCompletedEventHandler(object sender, EvaluateFlatFileEventArgs e);
        public delegate void FixFlatFileCompletedEventHandler(object sender, FixFlatFileEventArgs e);
        public delegate void DataDotGovCompletedEventHandler(object sender, DataDotGovEventArgs e);

        // create events
        public event MessageEventHandler Message;
        public event EnvironmentEventHandler Environment;
        public event PDFReportsCompletedEventHandler PDFReportsCompleted;
        public event EvaluateFlatFileCompletedEventHandler EvaluateFlatFileCompleted;
        public event FixFlatFileCompletedEventHandler FixFlatFileCompleted;
        public event DataDotGovCompletedEventHandler DataDotGovCompleted;

        #region VETS-4212 Defined
        // define constants for 4212 data file
        private enum _vets4212RecordColumns : int {
            id = 0,
            name = 1,
            length = 2,
            required = 3,
            validation = 4,
            errorMessage = 5,
            cleanStatement = 6 
        };
        private enum _vets4212Fields : int
        {
            companyNumber = 0,
            typeOfReporting = 1,
            typeOfForm = 2,
            numberOfMSCLocations = 3,
            periodEnding = 4,
            parentCompanyName = 5,
            parentCompanyStreet = 6,
            parentCompanyStreet2 = 7,
            parentCompanyCity = 8,
            parentCompanyCounty = 9,
            parentCompanyState = 10,
            parentCompanyZipcode = 11,
            companyContact = 12,
            companyContactTelephone = 13,
            compoanyContactEmail = 14,
            hiringLocationName = 15,
            hiringLocationStreet = 16,
            hiringLocationStreet2 = 17,
            hiringLocationCity = 18,
            hiringLocationCounty = 19,
            hiringLocationState = 20,
            hiringLocationZipcode = 21,
            NAICS = 22,
            DUNS = 23,
            FEIN = 24,
            NumEmps_ProtectedVeterans1 = 25,
            NumEmps_ProtectedVeterans2 = 26,
            NumEmps_ProtectedVeterans3 = 27,
            NumEmps_ProtectedVeterans4 = 28,
            NumEmps_ProtectedVeterans5 = 29,
            NumEmps_ProtectedVeterans6 = 30,
            NumEmps_ProtectedVeterans7 = 31,
            NumEmps_ProtectedVeterans8 = 32,
            NumEmps_ProtectedVeterans9 = 33,
            NumEmps_ProtectedVeterans10 = 34,
            NumEmps_ProtectedVeterans11 = 35,
            NumEmps_TotalAllVeteransNonVeterans1 = 36,
            NumEmps_TotalAllVeteransNonVeterans2 = 37,
            NumEmps_TotalAllVeteransNonVeterans3 = 38,
            NumEmps_TotalAllVeteransNonVeterans4 = 39,
            NumEmps_TotalAllVeteransNonVeterans5 = 40,
            NumEmps_TotalAllVeteransNonVeterans6 = 41,
            NumEmps_TotalAllVeteransNonVeterans7 = 42,
            NumEmps_TotalAllVeteransNonVeterans8 = 43,
            NumEmps_TotalAllVeteransNonVeterans9 = 44,
            NumEmps_TotalAllVeteransNonVeterans10 = 45,
            NumEmps_TotalAllVeteransNonVeterans11 = 46,
            NewHire_ProtectedVeterans1 = 47,
            NewHire_ProtectedVeterans2 = 48,
            NewHire_ProtectedVeterans3 = 49,
            NewHire_ProtectedVeterans4 = 50,
            NewHire_ProtectedVeterans5 = 51,
            NewHire_ProtectedVeterans6 = 52,
            NewHire_ProtectedVeterans7 = 53,
            NewHire_ProtectedVeterans8 = 54,
            NewHire_ProtectedVeterans9 = 55,
            NewHire_ProtectedVeterans10 = 56,
            NewHire_ProtectedVeterans11 = 57,
            NewHire_TotalAllVeteransNonVeterans1 = 58,
            NewHire_TotalAllVeteransNonVeterans2 = 59,
            NewHire_TotalAllVeteransNonVeterans3 = 60,
            NewHire_TotalAllVeteransNonVeterans4 = 61,
            NewHire_TotalAllVeteransNonVeterans5 = 62,
            NewHire_TotalAllVeteransNonVeterans6 = 63,
            NewHire_TotalAllVeteransNonVeterans7 = 64,
            NewHire_TotalAllVeteransNonVeterans8 = 65,
            NewHire_TotalAllVeteransNonVeterans9 = 66,
            NewHire_TotalAllVeteransNonVeterans10 = 67,
            NewHire_TotalAllVeteransNonVeterans11 = 68,
            Maximum = 69,
            Minimum = 70
        };
        private enum _vets100AFields : int
        {
            ReportID = 0,
            ReportType = 1,
            FilingCycle = 2,
            EndingPeriod = 3,
            FormType = 4,
            OrgType = 5,
            CoName = 6,
            CoAddress = 7,
            CoCity = 8,
            CoCounty = 9,
            CoState = 10,
            CoZip = 11,
            HlName = 12,
            HlAddress = 13,
            HlCity = 14,
            HlCounty = 15,
            HlState = 16,
            HlZip = 17,
            NAICS = 18,
            MSC = 19,
            MaxNumber = 20,
            MinNumber = 21,
            CreateOn = 22,
            JobCategCode1 = 23,
            NumEmps_OtherProtectedVets1 = 24,
            NewHire_OtherProtectedVets1 = 25,
            NumEmps_ArmedForcesServiceMedalVets1 = 26,
            NewHire_ArmedForcesServiceMedalVets1 = 27,
            NumEmps_RecentlySeparatedVets1 = 28,
            NewHire_RecentlySeparatedVets1 = 29,
            NumEmps_TotalAllEmployees1 = 30,
            NewHire_TotalAllEmployees1 = 31,
            JobCategCode2 = 32,
            NumEmps_OtherProtectedVets2 = 33,
            NewHire_OtherProtectedVets2 = 34,
            NumEmps_ArmedForcesServiceMedalVets2 = 35,
            NewHire_ArmedForcesServiceMedalVets2 = 36,
            NumEmps_RecentlySeparatedVets2 = 37,
            NewHire_RecentlySeparatedVets2 = 38,
            NumEmps_TotalAllEmployees2 = 39,
            NewHire_TotalAllEmployees2 = 40,
            JobCategCode3 = 41,
            NumEmps_OtherProtectedVets3 = 42,
            NewHire_OtherProtectedVets3 = 43,
            NumEmps_ArmedForcesServiceMedalVets3 = 44,
            NewHire_ArmedForcesServiceMedalVets3 = 45,
            NumEmps_RecentlySeparatedVets3 = 46,
            NewHire_RecentlySeparatedVets3 = 47,
            NumEmps_TotalAllEmployees3 = 48,
            NewHire_TotalAllEmployees3 = 49,
            JobCategCode4 = 50,
            NumEmps_OtherProtectedVets4 = 51,
            NewHire_OtherProtectedVets4 = 52,
            NumEmps_ArmedForcesServiceMedalVets4 = 53,
            NewHire_ArmedForcesServiceMedalVets4 = 54,
            NumEmps_RecentlySeparatedVets4 = 55,
            NewHire_RecentlySeparatedVets4 = 56,
            NumEmps_TotalAllEmployees4 = 57,
            NewHire_TotalAllEmployees4 = 58,
            JobCategCode5 = 59,
            NumEmps_OtherProtectedVets5 = 60,
            NewHire_OtherProtectedVets5 = 61,
            NumEmps_ArmedForcesServiceMedalVets5 = 62,
            NewHire_ArmedForcesServiceMedalVets5 = 63,
            NumEmps_RecentlySeparatedVets5 = 64,
            NewHire_RecentlySeparatedVets5 = 65,
            NumEmps_TotalAllEmployees5 = 66,
            NewHire_TotalAllEmployees5 = 67,
            JobCategCode7 = 68,
            NumEmps_OtherProtectedVets7 = 69,
            NewHire_OtherProtectedVets7 = 70,
            NumEmps_ArmedForcesServiceMedalVets7 = 71,
            NewHire_ArmedForcesServiceMedalVets7 = 72,
            NumEmps_RecentlySeparatedVets7 = 73,
            NewHire_RecentlySeparatedVets7 = 74,
            NumEmps_TotalAllEmployees7 = 75,
            NewHire_TotalAllEmployees7 = 76,
            JobCategCode8 = 77,
            NumEmps_OtherProtectedVets8 = 78,
            NewHire_OtherProtectedVets8 = 79,
            NumEmps_ArmedForcesServiceMedalVets8 = 80,
            NewHire_ArmedForcesServiceMedalVets8 = 81,
            NumEmps_RecentlySeparatedVets8 = 82,
            NewHire_RecentlySeparatedVets8 = 83,
            NumEmps_TotalAllEmployees8 = 84,
            NewHire_TotalAllEmployees8 = 85,
            JobCategCode9 = 86,
            NumEmps_OtherProtectedVets9 = 87,
            NewHire_OtherProtectedVets9 = 88,
            NumEmps_ArmedForcesServiceMedalVets9 = 89,
            NewHire_ArmedForcesServiceMedalVets9 = 90,
            NumEmps_RecentlySeparatedVets9 = 91,
            NewHire_RecentlySeparatedVets9 = 92,
            NumEmps_TotalAllEmployees9 = 93,
            NewHire_TotalAllEmployees9 = 94,
            JobCategCode10 = 95,
            NumEmps_OtherProtectedVets10 = 96,
            NewHire_OtherProtectedVets10 = 97,
            NumEmps_ArmedForcesServiceMedalVets10 = 98,
            NewHire_ArmedForcesServiceMedalVets10 = 99,
            NumEmps_RecentlySeparatedVets10 = 100,
            NewHire_RecentlySeparatedVets10 = 101,
            NumEmps_TotalAllEmployees10 = 102,
            NewHire_TotalAllEmployees10 = 103,
            JobCategCode11 = 104,
            NumEmps_OtherProtectedVets11 = 105,
            NewHire_OtherProtectedVets11 = 106,
            NumEmps_ArmedForcesServiceMedalVets11 = 107,
            NewHire_ArmedForcesServiceMedalVets11 = 108,
            NumEmps_RecentlySeparatedVets11 = 109,
            NewHire_RecentlySeparatedVets11 = 110,
            NumEmps_TotalAllEmployees11 = 111,
            NewHire_TotalAllEmployees11 = 112
        }
        private enum _vets4212HiringLocationFields : int
        {
            CompanyNumber = 0,
            LocationNumber = 1,
            LocationName = 2,
            LocationStreet = 3,
            LocationCity = 4,
            LocationState = 5,
            LocationZipcode = 6,
            LocationDUNS = 7,
            LocationNAICS = 8
        };

        // define 4212 hiring locations with sizes, required, validation, and error message
        private object[] _vets4212HlRecord = { new object[] { "DE-BHL-1", "Company Number", 7, true, @"^[A-Z]{1}[0-9]{6}$", "{0} Invalid: should start with 'T' followed by 7 digits", @"[^A-Z0-9]" },
                                            new object[] { "DE-BHL-2", "Hiring Location Number", 100, false, @"^[a-zA-Z0-9 \'\&\.\(\)\-]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters dash(-), period (.), parenthesis (()), ampersand (&), apostrophe (‘)", @"[^a-zA-Z0-9 \'\&\.\(\)\-]" },
                                            new object[] { "DE-BHL-3", "Hiring Location Name", 100, true, @"^[a-zA-Z0-9 \'\&\.\(\)\-]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters dash(-), period (.), parenthesis (()), ampersand (&), apostrophe (‘)", @"[^a-zA-Z0-9 \'\&\.\(\)\-]" },
                                            new object[] { "DE-BHL-4", "Hiring Location Street", 100, true, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\&\.\(\)\-]" },
                                            new object[] { "DE-BHL-5", "Hiring Location City", 50, true, @"^[a-zA-Z0-9 \'\-\.]{1,50}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                            new object[] { "DE-BHL-6", "Hiring Location State", 2, true, @"(AA|AE|AK|AL|AP|AR|AS|AZ|CA|CO|CT|DC|DE|FL|FM|FP|GA|GU|HI|IA|ID|IL|IN|KS|KY|LA|MA|MD|ME|MH|MI|MN|MP|MO|MS|MT|NC|ND|NE|NH|NJ|NM|NV|NY|OH|OK|OR|PA|PR|PW|RI|SC|SD|TN|TX|UT|VA|VI|VT|WA|WI|WV|WY)", "{0} Invalid: should be two character value for state", @"" },
                                            new object[] { "DE-BHL-7", "Hiring Location Zipcode", 10, true, @"^[0-9]{5,9}$", "{0} Invalid: should be 5 or 9 numeric digits", @"[^0-9]" },
                                            new object[] { "DE-BHL-8", "Hiring Location DUNS", 9, true, @"^[0-9]{9}$", "{0} Invalid: should be all numeric, length 9 digits", @"[^0-9]" },
                                            new object[] { "DE-BHL-9", "Hiring Location NAICS", 10, true, @"^[0-9]{6}$", "{0} Invalid: should be all numeric, length 6 digits", @"[^0-9]" }
                                            };

        // define 4212 field with sizes, required,  validation, and error message
        private object[] _vets4212Record = { new object[] { "DE-C4212-1", "Compnay Number", 7, true, @"^[A-Z]{1}[0-9]{6}$", "{0} Invalid: should start with 'T' followed by 7 digits", @"[^A-Z0-9]" },
                                  new object[] { "DE-C4212-2", "Type of Reporting Organization", 1, true, @"(P|S|B)", "{0} Invalid: should be P, S, or B", @"" },
                                  new object[] { "DE-C4212-3", "Type of Form", 3, true, @"(S|MHQ|MHL|MSC)", "{0} Invalid: should be S, MHQ, MHL, or MSC", @"" },
                                  new object[] { "DE-C4212-4", "Number of MSC Locations", 8, false, @"^[0-9]{0,8}$", "{0} locations Invalid: should be numeric value with maximum of 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-5", "Twelve Month Period Ending", 10, true, @"^([0-9]{1,2}/[0-9]{1,2}/[0-9]{4})$", "{0} Invalid: should be MM/DD/YYY format", @"[^0-9/]" },
                                  new object[] { "DE-C4212=6", "Parent Company Name", 100, true, @"^[a-zA-Z0-9 \'\&\.\(\)\-]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters dash(-), period (.), parenthesis (()), ampersand (&), apostrophe (‘)", @"[^a-zA-Z0-9 \'\&\.\(\)\-]" },
                                  new object[] { "DE-C4212-7", "Company Street Address", 100, true, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-8", "Company Second Street Address", 100, false, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-9", "Company City", 50, true, @"^[a-zA-Z0-9 \'\-\.]{1,50}$", "{0} Invalid: should be alpha-numeric, max length 50, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-10", "Company County", 50, true, @"^[a-zA-Z0-9 \'\-\.]{1,50}$", "{0} Invalid: should be alpha-numeric, max length 50, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-11", "Company State", 2, true, @"(AA|AE|AK|AL|AP|AR|AS|AZ|CA|CO|CT|DC|DE|FL|FM|FP|GA|GU|HI|IA|ID|IL|IN|KS|KY|LA|MA|MD|ME|MH|MI|MN|MP|MO|MS|MT|NC|ND|NE|NH|NJ|NM|NV|NY|OH|OK|OR|PA|PR|PW|RI|SC|SD|TN|TX|UT|VA|VI|VT|WA|WI|WV|WY)", "{0} Invalid: should be two character value for state", @"" },
                                  new object[] { "DE-C4212-12", "Company Zipcode", 9, true, @"^[0-9]{5,9}$", "{0} Invalid: should be 5 or 9 numeric digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-13", "Name of Company Contact", 100, true, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-14", "Telephone for Company Contact", 20, true, @"^[0-9]{10,20}$", "{0} Telephone Invalid: should be numeric, min length 10, max length 20", @"[^0-9]" },
                                  new object[] { "DE-C4212-15", "Email for Company Contact", 100, true, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$", "{0} Invalid: should be valid email address", @"[^a-zA-Z0-9\#\-\_\~\!\$\&'\(\)\*\+\,\;\=\:\.]" },
                                  new object[] { "DE-C4212=16", "Hiring Locatio Name", 100, false, @"^[a-zA-Z0-9 \.\'\&\(\)\-]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, speical characters dash(-), period (.), parenthesis (()), ampersand (&), apostrophe (‘)", @"[^a-zA-Z0-9 \.\'\&\(\)\-]" },
                                  new object[] { "DE-C4212-17", "Hiring Location Street Address", 100, false, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, speical characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-18", "Hiring Location Second Street Address", 100, false, @"^[a-zA-Z0-9 \'\-\.]{1,100}$", "{0} Invalid: should be alpha-numeric, max length 100, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-19", "Hiring Location City", 50, false, @"^[a-zA-Z0-9 \'\-\.]{1,50}$", "{0} Invalid: should be alpha-numeric, max length 50, special characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-20", "Hiring Location County", 50, false, @"^[a-zA-Z0-9 \'\-\.]{1,50}$", "{0} Invalid: should be alpha-numeric, max length 50, speical characters (‘),dash(-), period (.)", @"[^a-zA-Z0-9 \'\-\.]" },
                                  new object[] { "DE-C4212-21", "Hiring Location State", 2, false, @"(AA|AE|AK|AL|AP|AR|AS|AZ|CA|CO|CT|DC|DE|FL|FM|FP|GA|GU|HI|IA|ID|IL|IN|KS|KY|LA|MA|MD|ME|MH|MI|MN|MP|MO|MS|MT|NC|ND|NE|NH|NJ|NM|NV|NY|OH|OK|OR|PA|PR|PW|RI|SC|SD|TN|TX|UT|VA|VI|VT|WA|WI|WV|WY)", "{0} Invalid: should be two character value for state", @"" },
                                  new object[] { "DE-C4212-22", "Hiring Location Zipcode", 9, false, @"^[0-9]{5,9}$", "{0} Invalid: should be 5 or 9 numeric digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-23", "NAICS", 10, true, @"^[0-9]{5,10}$", "{0} Invalid: should be all numeric, length 6 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-24", "DUNS", 9, true, @"^[0-9]{9}$", "{0} Invalid: should be all numeric, length 9 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-25", "Federal Employer Identication Number (FEIN)", 9, true, @"^[0-9]{9}$", "{0} Invalid: should be all numeric, length 9 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-26", "Number of protected veterans who are Executive/Senior Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-27", "Number of Protected Veterans employees who are First/Mid Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-28", "Number of protected Veterans employees who are Professionals", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-29", "Number of protected Veterans employees who are Technicians", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-30", "Number of protected Veterans employees who are Sales Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-31", "Number of protected Veterans employees who are Administrative Support Workers", 8, true, @"^[0-9]{1,8}$","{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-32", "Number of protected Veterans employees who are Craft Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-33", "Number of protected Veterans employees who are Operatives", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-34", "Number of protected Veterans employees  who are Laborers / Helpers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-35", "Number of protected Veterans employees who are Service Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-36", "Total Number of protected Veterans employees", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, sum of all values in column Protected Veterans", @"[^0-9]" },
                                  new object[] { "DE-C4212-37", "Total Number of Veterans and Non-Veterans employees who are Executive/Senior Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-38", "Total Number of Veterans and Non-Veterans employees who are First/Mid Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-39", "Total Number of Veterans and Non-Veterans employees who are Professionals", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-40", "Total Number of Veterans and Non-Veterans employees who are Technicians", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-41", "Total Number of Veterans and Non-Veterans employees who are Sales Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-42", "Total Number of Veterans and Non-Veterans employees who are Administrative Support Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-43", "Total Number of Veterans and Non-Veterans employees who are Craft Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-44", "Total Number of Veterans and Non-Veterans employees who are Operatives", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-45", "Total Number of Veterans and Non-Veterans employees who are Laborers/ Helpers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-46", "Total Number of Veterans and Non-Veterans employees who are Service Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-47", "Total Number of Veterans and Non-Veterans employees ", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value and sum of all values in Total All Veterans Non Veterans Column", @"[^0-9]" },
                                  new object[] { "DE-C4212-48", "Number of protected Veterans new hires who are Executive/Senior Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-49", "Number of protected Veterans new hires who are First/Mid Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-50", "Number of protected Veterans new hires who are Professionals", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-51", "Number of protected Veterans new hires who are Technicians", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-52", "Number of protected Veterans new hires who are Sales Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-53", "Number of protected Veterans new hires who are Administrative Support Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-54", "Number of protected Veterans new hires who are Craft Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-55", "Number of protected Veterans new hires who are Operatives", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-56", "Number of protected Veterans new hires who are Laborers / Helpers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-57", "Number of protected Veterans new hires who are Service Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-58", "Total Number of protected Veterans new hires", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-59", "Total Number of Veteran and Non-Veteran new hires who are Executive/Senior Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-60", "Total Number of Veteran and Non-Veteran new hires who are First/Mid Level Officials and Managers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-61", "Total Number of Veteran and Non-Veteran new hires who are Professionals", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-62", "Total Number of Veteran and Non-Veteran new hires who are Technicians", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-63", "Total Number of Veteran and Non-Veteran new hires who are Sales Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-64", "Total Number of Veteran and Non-Veteran new hires who are Administrative Support Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-65", "Total Number of Veteran and Non-Veteran new hires who are Craft Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-66", "Total Number of Veteran and Non-Veteran new hires who are Operatives", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-67", "Total Number of Veteran and Non-Veteran new hires who are Laborers / Helpers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-68", "Total Number of Veteran and Non-Veteran new hires who are Service Workers", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-69", "Total Number of Veterans and Non-Veteran new hires", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits, equal or greater than protected veterans value", @"[^0-9]" },
                                  new object[] { "DE-C4212-70", "Maximum Number", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" },
                                  new object[] { "DE-C4212-71", "Minimum Number", 8, true, @"^[0-9]{1,8}$", "{0} Invalid: should be all numeric, max length 8 digits", @"[^0-9]" } 
                                  };

        // VETS4212 data elements returned by OPA API
        string[] _vets4212Columns = { "ReportID", "ReportType", "FilingCycle", "EndingPeriod", "FormType", "OrgType", "CoName", "CoAddress", "CoCity", "CoCounty", "CoState", "CoZip", 
                                      "HlName", "HlAddress", "HlCity", "HlCounty", "HlState", "HlZip", "NAICS", "MSC", "MaxNumber", "MinNumber", "CreatedOn", 
                                      "NumEmps_ProtectedVets1", "NewHire_ProtectedVets1", "NumEmps_TotalAllEmployees1", "NewHire_TotalAllEmployees1", 
                                      "JobCategCode2", "NumEmps_ProtectedVets2", "NewHire_ProtectedVets2", "NumEmps_TotalAllEmployees2", "NewHire_TotalAllEmployees2", 
                                      "JobCategCode3", "NumEmps_ProtectedVets3", "NewHire_ProtectedVets3", "NumEmps_TotalAllEmployees3", "NewHire_TotalAllEmployees3", 
                                      "JobCategCode4", "NumEmps_ProtectedVets4", "NewHire_ProtectedVets4", "NumEmps_TotalAllEmployees4", "NewHire_TotalAllEmployees4", 
                                      "JobCategCode5", "NumEmps_ProtectedVets5", "NewHire_ProtectedVets5", "NumEmps_TotalAllEmployees5", "NewHire_TotalAllEmployees5", 
                                      "JobCategCode6", "NumEmps_ProtectedVets6", "NewHire_ProtectedVets6", "NumEmps_TotalAllEmployees6", "NewHire_TotalAllEmployees6", 
                                      "JobCategCode7", "NumEmps_ProtectedVets7", "NewHire_ProtectedVets7", "NumEmps_TotalAllEmployees7", "NewHire_TotalAllEmployees7", 
                                      "JobCategCode8", "NumEmps_ProtectedVets8", "NewHire_ProtectedVets8", "NumEmps_TotalAllEmployees8", "NewHire_TotalAllEmployees8", 
                                      "JobCategCode9", "NumEmps_ProtectedVets9", "NewHire_ProtectedVets9", "NumEmps_TotalAllEmployees9", "NewHire_TotalAllEmployees9", 
                                      "JobCategCode10", "NumEmps_ProtectedVets10", "NewHire_ProtectedVets10", "NumEmps_TotalAllEmployees10", "NewHire_TotalAllEmployees10", 
                                      "JobCategCode11", "NumEmps_ProtectedVets11", "NewHire_ProtectedVets11", "NumEmps_TotalAllEmployees11", "NewHire_TotalAllEmployees11" 
                                      };

        // VETS100A data elements returned by OPA API
        string[] _vets100AColumns = { "ReportID", "ReportType", "FilingCycle", "EndingPeriod", "FormType", "OrgType", "CoName", "CoAddress", "CoCity", "CoCounty", "CoState", "CoZip", 
                                      "HlName", "HlAddress", "HlCity", "HlCounty", "HlState", "HlZip", "NAICS", "MSC", "MaxNumber", "MinNumber", "CreatedOn",
                                      "JobCategCode1", "NumEmps_OtherProtectedVets1", "NewHire_OtherProtectedVets1", "NumEmps_ArmedForcesServiceMedalVets1", "NewHire_ArmedForcesServiceMedalVets1", 
                                      "NumEmps_RecentlySeparatedVets1", "NewHire_RecentlySeparatedVets1", "NumEmps_TotalAllEmployees1", "NewHire_TotalAllEmployees1", 
                                      "JobCategCode2", "NumEmps_OtherProtectedVets2", "NewHire_OtherProtectedVets2", "NumEmps_ArmedForcesServiceMedalVets2", "NewHire_ArmedForcesServiceMedalVets2", 
                                      "NumEmps_RecentlySeparatedVets2", "NewHire_RecentlySeparatedVets2", "NumEmps_TotalAllEmployees2", "NewHire_TotalAllEmployees2", 
                                      "JobCategCode3", "NumEmps_OtherProtectedVets3", "NewHire_OtherProtectedVets3", "NumEmps_ArmedForcesServiceMedalVets3", "NewHire_ArmedForcesServiceMedalVets3", 
                                      "NumEmps_RecentlySeparatedVets3", "NewHire_RecentlySeparatedVets3", "NumEmps_TotalAllEmployees3", "NewHire_TotalAllEmployees3", 
                                      "JobCategCode4", "NumEmps_OtherProtectedVets4", "NewHire_OtherProtectedVets4", "NumEmps_ArmedForcesServiceMedalVets4", "NewHire_ArmedForcesServiceMedalVets4", 
                                      "NumEmps_RecentlySeparatedVets4", "NewHire_RecentlySeparatedVets4", "NumEmps_TotalAllEmployees4", "NewHire_TotalAllEmployees4", 
                                      "JobCategCode5", "NumEmps_OtherProtectedVets5", "NewHire_OtherProtectedVets5", "NumEmps_ArmedForcesServiceMedalVets5", "NewHire_ArmedForcesServiceMedalVets5", 
                                      "NumEmps_RecentlySeparatedVets5", "NewHire_RecentlySeparatedVets5", "NumEmps_TotalAllEmployees5", "NewHire_TotalAllEmployees5", 
                                      "JobCategCode6", "NumEmps_OtherProtectedVets6", "NewHire_OtherProtectedVets6", "NumEmps_ArmedForcesServiceMedalVets6", "NewHire_ArmedForcesServiceMedalVets6", 
                                      "NumEmps_RecentlySeparatedVets6", "NewHire_RecentlySeparatedVets6", "NumEmps_TotalAllEmployees6", "NewHire_TotalAllEmployees6", 
                                      "JobCategCode7", "NumEmps_OtherProtectedVets7", "NewHire_OtherProtectedVets7", "NumEmps_ArmedForcesServiceMedalVets7", "NewHire_ArmedForcesServiceMedalVets7", 
                                      "NumEmps_RecentlySeparatedVets7", "NewHire_RecentlySeparatedVets7", "NumEmps_TotalAllEmployees7", "NewHire_TotalAllEmployees7", 
                                      "JobCategCode8", "NumEmps_OtherProtectedVets8", "NewHire_OtherProtectedVets8", "NumEmps_ArmedForcesServiceMedalVets8", "NewHire_ArmedForcesServiceMedalVets8", 
                                      "NumEmps_RecentlySeparatedVets8", "NewHire_RecentlySeparatedVets8", "NumEmps_TotalAllEmployees8", "NewHire_TotalAllEmployees8", 
                                      "JobCategCode9", "NumEmps_OtherProtectedVets9", "NewHire_OtherProtectedVets9", "NumEmps_ArmedForcesServiceMedalVets9", "NewHire_ArmedForcesServiceMedalVets9", 
                                      "NumEmps_RecentlySeparatedVets9", "NewHire_RecentlySeparatedVets9", "NumEmps_TotalAllEmployees9", "NewHire_TotalAllEmployees9", 
                                      "JobCategCode10", "NumEmps_OtherProtectedVets10", "NewHire_OtherProtectedVets10", "NumEmps_ArmedForcesServiceMedalVets10", "NewHire_ArmedForcesServiceMedalVets10", 
                                      "NumEmps_RecentlySeparatedVets10", "NewHire_RecentlySeparatedVets10", "NumEmps_TotalAllEmployees10", "NewHire_TotalAllEmployees10", 
                                      "JobCategCode11", "NumEmps_OtherProtectedVets11", "NewHire_OtherProtectedVets11", "NumEmps_ArmedForcesServiceMedalVets11", "NewHire_ArmedForcesServiceMedalVets11", 
                                      "NumEmps_RecentlySeparatedVets11", "NewHire_RecentlySeparatedVets11", "NumEmps_TotalAllEmployees11", "NewHire_TotalAllEmployees11" };

        // VETS100 data elements returned by OPA API
        string[] _vets100Columns = { "ReportID", "ReportType", "FilingCycle", "EndingPeriod", "FormType", "OrgType", "CoName", "CoAddress", "CoCity", "CoCounty", "CoState", "CoZip", 
                                      "HlName", "HlAddress", "HlCity", "HlCounty", "HlState", "HlZip", "NAICS", "MSC", "MaxNumber", "MinNumber", "CreatedOn", 
                                      "JobCategCode1", "NumEmps_VietNamEraVets1", "NewHire_VietnameEraVets1", "NumEmps_OtherProtectedVets1", "NewHire_OtherProtectedVets1", 
                                      "NumEmps_RecentlySeparatedVets1", "NewHire_RecentlySeparatedVets1", "NewHire_TotalAllEmployees1",
                                      "JobCategCode1", "NumEmps_VietNamEraVets2", "NewHire_VietnameEraVets2", "NumEmps_OtherProtectedVets2", "NewHire_OtherProtectedVets2", 
                                      "NumEmps_RecentlySeparatedVets2", "NewHire_RecentlySeparatedVets2", "NewHire_TotalAllEmployees2",
                                      "JobCategCode1", "NumEmps_VietNamEraVets3", "NewHire_VietnameEraVets3", "NumEmps_OtherProtectedVets3", "NewHire_OtherProtectedVets3", 
                                      "NumEmps_RecentlySeparatedVets3", "NewHire_RecentlySeparatedVets3", "NewHire_TotalAllEmployees3",
                                      "JobCategCode1", "NumEmps_VietNamEraVets4", "NewHire_VietnameEraVets4", "NumEmps_OtherProtectedVets4", "NewHire_OtherProtectedVets4", 
                                      "NumEmps_RecentlySeparatedVets4", "NewHire_RecentlySeparatedVets4", "NewHire_TotalAllEmployees4",
                                      "JobCategCode1", "NumEmps_VietNamEraVets5", "NewHire_VietnameEraVets5", "NumEmps_OtherProtectedVets5", "NewHire_OtherProtectedVets5", 
                                      "NumEmps_RecentlySeparatedVets5", "NewHire_RecentlySeparatedVets5", "NewHire_TotalAllEmployees5",
                                      "JobCategCode1", "NumEmps_VietNamEraVets6", "NewHire_VietnameEraVets6", "NumEmps_OtherProtectedVets6", "NewHire_OtherProtectedVets6", 
                                      "NumEmps_RecentlySeparatedVets6", "NewHire_RecentlySeparatedVets6", "NewHire_TotalAllEmployees6",
                                      "JobCategCode1", "NumEmps_VietNamEraVets7", "NewHire_VietnameEraVets7", "NumEmps_OtherProtectedVets7", "NewHire_OtherProtectedVets7", 
                                      "NumEmps_RecentlySeparatedVets7", "NewHire_RecentlySeparatedVets7", "NewHire_TotalAllEmployees7",
                                      "JobCategCode1", "NumEmps_VietNamEraVets8", "NewHire_VietnameEraVets8", "NumEmps_OtherProtectedVets8", "NewHire_OtherProtectedVets8", 
                                      "NumEmps_RecentlySeparatedVets8", "NewHire_RecentlySeparatedVets8", "NewHire_TotalAllEmployees8",
                                      "JobCategCode1", "NumEmps_VietNamEraVets9", "NewHire_VietnameEraVets9", "NumEmps_OtherProtectedVets9", "NewHire_OtherProtectedVets9", 
                                      "NumEmps_RecentlySeparatedVets9", "NewHire_RecentlySeparatedVets9", "NewHire_TotalAllEmployees9",
                                      "JobCategCode1", "NumEmps_VietNamEraVets10", "NewHire_VietnameEraVets10", "NumEmps_OtherProtectedVets10", "NewHire_OtherProtectedVets10", 
                                      "NumEmps_RecentlySeparatedVets10", "NewHire_RecentlySeparatedVets10", "NewHire_TotalAllEmployees10" };

        #endregion
        public vets4212()
        {
        }

        #region Event Handlers
        /// <summary>
        /// Raises an event of type message, contains information from application process
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">TypeOf MessageEventArgs</param>
        protected virtual void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                MessageEventHandler handler = Message;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        /// <summary>
        /// Raises an event during creation of PDF documents gaining access to envirnmental resources
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">TypeOf EnvironmentEventArgs</param>
        protected virtual void OnEnvironment(object sender, EnvironmentEventArgs e)
        {
            try
            {
                EnvironmentEventHandler handler = Environment;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        /// <summary>
        /// Raises an event when creation of the PDF reports have been completed
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">TypeOf PDFEventArgs</param>
        protected virtual void OnPdfReportsCompleted(object sender, PDFEventArgs e)
        {
            try
            {
                PDFReportsCompletedEventHandler handler = PDFReportsCompleted;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        /// <summary>
        /// Raises an event when evaluation of batch file process has been completed
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">TypeOf EvaluateFlatFileArgs</param>
        protected virtual void OnEvaluateFlatFileCompleted(object sender, EvaluateFlatFileEventArgs e)
        {
            try
            {
                EvaluateFlatFileCompletedEventHandler handler = EvaluateFlatFileCompleted;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        /// <summary>
        /// Raises an event when fixing batch file has been completed.
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">TypeOf FixFlatFileArgs</param>
        protected virtual void OnFixFlatFileCompleted(object sender, FixFlatFileEventArgs e)
        {
            try
            {
                FixFlatFileCompletedEventHandler handler = FixFlatFileCompleted;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        /// <summary>
        /// Raises an event when has completed retrieving all requested DataDotGov records
        /// </summary>
        /// <param name="sender">this</param>
        /// <param name="e">Type of DataDotGovArgs</param>
        protected virtual void OnDataDotGovCompleted(object sender, DataDotGovEventArgs e)
        {
            try
            {
                DataDotGovCompletedEventHandler handler = DataDotGovCompleted;
                if (handler != null)
                {
                    handler(sender, e);
                }
            }
            catch
            {
                // handle exception if needed
            }
        }
        #endregion

        #region private methods
        /// <summary>
        /// Using URL filtering get specific data from DataDotGov based on report type, filing cycle, company name, and NAICS
        /// </summary>
        /// <param name="Filename">The path to save the retrieved data</param>
        /// <param name="ReportType">V100, V100A, or V4212, determine the dataset table we are querring.</param>
        /// <param name="CompanyName">Partial company name we will be filtering the received data.</param>
        /// <param name="NAICS">Partial NAICS we will be filtering the recieved data.</param>
        /// <param name="DataDotGovUrl">The full URL used to get requested data.</param>
        private void getReportDataFromDataDotGov(string Filename, string ReportType, string CompanyName, string NAICS, string DataDotGovUrl)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;

            // using an array to store and return the ReportIDs
            ArrayList ReportIDs = new ArrayList();

            // create TextWriter for output file
            FileStream fileHandle = new FileStream(Filename, FileMode.Create,  FileAccess.ReadWrite, FileShare.Read);
            StreamWriter file = new StreamWriter(fileHandle);

            // write the file header
            if (ReportType == "V100A")
                file.WriteLine(string.Join(",", _vets100AColumns));
            else if (ReportType == "V100")
                file.WriteLine(string.Join(",", _vets100Columns));
            else
                file.WriteLine(string.Join(",", _vets4212Columns));

            try
            {
                // configure for the skip records
                int skip = 0;

                // keep getting each row of data until we have all requested.
                while ((ReportIDs.Count % 100) == 0)
                {
                    // set the skip amount
                    Uri url = new Uri(string.Format(DataDotGovUrl, skip));

                    // get results of query
                    request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    request.KeepAlive = true;

                    // get the response
                    response = request.GetResponse();
                    if (request.HaveResponse)
                    {
                        // display message retrieving
                        OnMessage(this, new MessageEventArgs(String.Format("Received {0} Report's", ReportIDs.Count)));

                        // get the response stream for reading
                        System.IO.Stream responseStream = response.GetResponseStream();
                        System.Text.Encoding encode = System.Text.Encoding.UTF8;

                        // reading using StreamReader
                        System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, encode);
                        System.Xml.XmlDocument results = new System.Xml.XmlDocument();
                        results.Load(reader);

                        // go through the Xml document and get each of the report IDs
                        System.Xml.XmlNode root = results.DocumentElement;
                        if (root.HasChildNodes)
                            getReportData(root, ReportType, CompanyName, NAICS, ref ReportIDs, ref file);

                        // release resources
                        if (reader != null)
                        {
                            // close and dispose
                            reader.Close();
                            reader.Dispose();
                            reader = null;
                        }

                        if (responseStream != null)
                        {
                            // close and dispose
                            responseStream.Close();
                            responseStream.Dispose();
                            responseStream = null;
                        }
                    }

                    // update skip
                    skip += 100;
                }
            }
            catch (Exception ex)
            {
                // unable to det reportIDs
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while trying to get report data from DataDotGov with message: {0}", ex.Message)));
            }
            finally
            {
                // close stream and return
                if (file != null)
                {
                    file.Flush();
                    file.Close();
                }

                // send message
                OnMessage(this, new MessageEventArgs("Completed generating file..."));
            }
        }
        /// <summary>
        /// Processes the retrieved reporting data saving only requested based on parameters
        /// called by "getReportDataFromDataDotGov"
        /// </summary>
        /// <param name="node">The root XmlNode</param>
        /// <param name="ReportType">V100, V100A, or V4212</param>
        /// <param name="CompanyName">Partial company name to filter retrieved data</param>
        /// <param name="NAICS">Partial NAICS to filter retrieved data</param>
        /// <param name="ReportIDs">Listing of all processed report identifiers</param>
        /// <param name="file">File stream to write filtered data</param>
        private void getReportData(System.Xml.XmlNode node, string ReportType, string CompanyName, string NAICS, ref ArrayList ReportIDs, ref StreamWriter file)
        {
            /***************************************************************************
             ** Data format as follows
             ** Root -> Entry -> Content
             **************************************************************************/
            try
            {
                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "entry")
                        locateContentNode(child, ReportType, CompanyName, NAICS, ref ReportIDs, ref file);
                }
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while getting report data with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// Processes the retrieved reporting data by locating the content node.
        /// called by "getReportData"
        /// </summary>
        /// <param name="node">Base node to start locating child content node</param>
        /// <param name="ReportType">V100, V100A, V4212</param>
        /// <param name="CompanyName">Partial company name to filter retrieved data</param>
        /// <param name="NAICS">Partial NAICS to filter retrieved data</param>
        /// <param name="ReportIDs">Listing of all processed report identifiers</param>
        /// <param name="file">File stream to write filtered data</param>
        private void locateContentNode(System.Xml.XmlNode node, string ReportType, string CompanyName, string NAICS, ref ArrayList ReportIDs, ref System.IO.StreamWriter file)
        {
            try
            {
                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "content")
                        locatePropertiesNode(child, ReportType, CompanyName, NAICS, ref ReportIDs, ref file);
                }
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while locating content node with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// Process the retrieved reporting data by locating the properties node.
        /// called by "locateContentNode"
        /// </summary>
        /// <param name="node">Base node to start locating child properties node</param>
        /// <param name="ReportType">V100, V100A, or V4212</param>
        /// <param name="CompanyName">Partial company name to filter retrieved data</param>
        /// <param name="NAICS">Partial NAICS to filter retrieved data</param>
        /// <param name="ReportIDs">Listing of all processed report identifiers</param>
        /// <param name="file">File stream to write filtered data</param>
        private void locatePropertiesNode (System.Xml.XmlNode node, string ReportType, string CompanyName, string NAICS, ref ArrayList ReportIDs, ref System.IO.StreamWriter file)
        {
            try
            {
                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    if (child.Name == "m:properties")
                        processPropertiesNode(child, ReportType, CompanyName, NAICS, ref ReportIDs, ref file);
                }
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while locating properties node with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// Processes the retrieved data by determining if it meets the requested properties, if so the record is then saved to the file stream.
        /// called by "locatePropertiesNode"
        /// </summary>
        /// <param name="node">Properties node to process</param>
        /// <param name="ReportType">V100, V100A, or V4212</param>
        /// <param name="CompanyName">Partial comapny name used to determine if we want to save this record</param>
        /// <param name="NAICS">Partial NAICS used to determine if we want to save this record</param>
        /// <param name="ReportIDs">Listing of all processed report identifiers</param>
        /// <param name="file">File stream to write filtered data</param>
        private void processPropertiesNode(System.Xml.XmlNode node, string ReportType, string CompanyName, string NAICS, ref ArrayList ReportIDs, ref System.IO.StreamWriter file)
        {
            // save each report data
            string[] reportData = null;
            int column = 0;
            bool addReport = true;

            try
            {
                // load data array based on report type
                if (ReportType == "V100A")
                    reportData = new string[_vets100AColumns.Length];
                else if (ReportType == "V100")
                    reportData = new string[_vets100Columns.Length];
                else
                    reportData = new string[_vets4212Columns.Length];

                // process the properties of record
                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    // only process if name like d:xxx
                    if (child.Name.Substring(0, 2) == "d:")
                    {
                        // Save report id for adding to array list
                        if (child.Name == "d:ReportID")
                            ReportIDs.Add(child.InnerText);
                        
                        // add column to reportData
                        if (ReportType == "V100A")
                            column = IndexOfField(_vets100AColumns, child.Name.Substring(2));
                        else if (ReportType == "V100")
                            column = IndexOfField(_vets100Columns, child.Name.Substring(2));
                        else
                            column = IndexOfField(_vets4212Columns, child.Name.Substring(2));

                        if (column != -1)
                            reportData[column] = child.InnerText;
                    }
                }

                // determine if we add this line based on CompanyName and NAICS
                if (!string.IsNullOrWhiteSpace(CompanyName))
                {
                    // get column for CoName
                    if (ReportType == "V100A")
                        column = IndexOfField(_vets100AColumns, "CoName");
                    else if (ReportType == "V100")
                        column = IndexOfField(_vets100Columns, "CoName");
                    else
                        column = IndexOfField(_vets4212Columns, "CoName");

                    // save company name
                    string CoName = reportData[column];

                    // get column for HlName
                    if (ReportType == "V100A")
                        column = IndexOfField(_vets100AColumns, "HlName");
                    else if (ReportType == "V100")
                        column = IndexOfField(_vets100Columns, "HlName");
                    else
                        column = IndexOfField(_vets4212Columns, "HlName");

                    // save hiring location name
                    string HlName = reportData[column];

                    // does it contain requested information
                    if (!CoName.Contains(CompanyName) && !HlName.Contains(CompanyName))
                        addReport = false;
                }

                // determine if we are looking for NAICS
                if (!string.IsNullOrWhiteSpace(NAICS))
                {
                    if (ReportType == "V100A")
                        column = IndexOfField(_vets100AColumns, "NAICS");
                    else if (ReportType == "V100")
                        column = IndexOfField(_vets100Columns, "NAICS");
                    else
                        column = IndexOfField(_vets4212Columns, "NAICS");

                    // does it contain requested number
                    if (reportData[column].Length < 6)
                        reportData[column] = reportData[column].PadLeft(6, '0');
                    if (reportData[column].Substring(0, NAICS.Length) != NAICS)
                        addReport = false;
                }

                if (addReport)
                {
                    // completed with report data, write to file
                    file.WriteLine(string.Join(",", reportData));
                }
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while processing properties nodes with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// Obtain location of requested column in array of column names
        /// </summary>
        /// <param name="columns">String array of column names</param>
        /// <param name="value">The column name to locate in the string array</param>
        private int IndexOfField(string[] columns, string value)
        {
            int index = 0;
            foreach (string column in columns) 
            {
                if (column == value)
                    return (index);
                index++;
            }
            return (-1);
        }
        /// <summary>
        /// Procedure used to get all requested ReportIDs to generate report PDFs
        /// Requires valid VETS-4212 user account
        /// </summary>
        /// <param name="state">Contains all required information to access website</param>
        /// <param name="cookies">Used for keeping current session/authentication information</param>
        /// <param name="ReportIDs">Resultant report identifiers returned</param>
        /// <returns></returns>
        private bool getReportIDsFromJSON(pdfStateObject state, ref System.Net.CookieContainer cookies, out ArrayList ReportIDs)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;
            System.IO.Stream responseStream = null;
            Password nd = new Password(12, 24, false, false, true, false);
            string JSONData = "";
            ReportIDs = new ArrayList();

            try
            {
                // perform the search
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(((pdfStateObject)state).reportSearchAddress);
                request.KeepAlive = true;
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                // we have an internal and external version of this search
                /*******************************************************************************************************************************************************************************
                 ** External Search URL: https://vets100.dol.gov/vets100/External/Report/ExecuteSearch
                 ** Internal Search URL: https://admintools2.dol.gov/VETS100/Internal/Report/ExecuteSearch
                /*******************************************************************************************************************************************************************************
                 ** External: _search=false&nd=1432327903902&rows=10&page=1&sidx=&sord=asc&FilterCriteria.CompanyNumber=T140375&FilterCriteria.FilingCycle=2014&FilterCriteria.FormType=
                 ** Internal: _search=false&nd=1432327903502&rows=10&page=1&sidx=&sord=asc&ReportId=&FilingCycle=2014&FormType=VETS100A&SubmittedBy=&SubmittedDate=&CompanyName=&CompanyNumber=T140375&EIN=&DUNS=&LocationName=&LocationState=TN&PointOfContact=
                 ******************************************************************************************************************************************************************************/
                string inputData = null;
                if (((pdfStateObject)state).InternalAccess)
                    inputData = string.Format("_search=false&nd={0}&rows=10&page=1&sidx=&sord=asc&ReportId=&FilingCycle={1}&FormType={2}&SubmittedBy=&SubmittedDate=&CompanyName={3}&CompanyNumber=&EIN=&DUNS=&LocationName=&LocationState=TN&PointOfContact=",
                        nd.CreatePassword(), ((pdfStateObject)state).FilingCycle, System.Web.HttpUtility.UrlEncode(((pdfStateObject)state).ReportType), System.Web.HttpUtility.UrlEncode(((pdfStateObject)state).CompanyName));
                else
                    inputData = string.Format("_search=false&nd={0}&rows=10&page=1&sidx=&sord=asc&FilterCriteria.CompanyNumber={1}&FilterCriteria.FilingCycle={2}&FilterCriteria.FormType={3}",
                        nd.CreatePassword(), ((pdfStateObject)state).CompanyNumber, ((pdfStateObject)state).FilingCycle, System.Web.HttpUtility.UrlEncode(((pdfStateObject)state).ReportType));

                 // encode data as ASCII
                //ASCIIEncoding encoding = new ASCIIEncoding();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                byte[] byteData = encoding.GetBytes(inputData);

                // set the content length
                request.ContentLength = byteData.Length;

                // get the request stream
                System.IO.Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteData, 0, byteData.Length);

                // get the response
                response = request.GetResponse();
                if (request.HaveResponse)
                {
                    // get the response stream
                    responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;

                    // reading using StreamReader
                    System.IO.TextReader reader = new System.IO.StreamReader(responseStream, encode);
                    JSONData = reader.ReadToEnd();

                    OnMessage(this, new MessageEventArgs("Retrieved initial report data."));

                    // release resources
                    if (reader != null)
                    {
                        // close and dispose
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }

                    if (responseStream != null)
                    {
                        // close and dispose
                        responseStream.Close();
                        responseStream.Dispose();
                        responseStream = null;
                    }
                }
                if (JSONData == "") return (false);

                // have the initial data need to determine the number of record for retrieve all
                // record format as follows
                // {"total":2,"page":2,"records":11,"rows":[{"ReportId":453744,"ReportType":"100A","FormType":"MHQ","FilingCycle":2012,"DateReceived":"2012-09-19","CompanyNumber":"T130241","CompanyName":"Hoth Endor, Inc.","LocationName":"Endor Moon","EIN":"571153311","DUNS":"179171731","ContactName":"Darth Delso","ContactPhone":"222-345-1234"}]}
                string numRecords = "";
                string[] records = JSONData.Split(',');

                // find the records record
                foreach (string record in records)
                {
                    string[] data = record.Split(':');
                    if (data[0] == "\"records\"")
                    {
                        numRecords = data[1];
                        break;
                    }
                }
                if (numRecords == "") return (false);

                // reset JSONData
                JSONData = "";

                // rebuild query using number of records to retrieve all records
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(((pdfStateObject)state).reportSearchAddress);
                request.KeepAlive = true;
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                /*******************************************************************************************************************************************************************************
                 ** External Search URL: https://vets100.dol.gov/vets100/External/Report/ExecuteSearch
                 ** Internal Search URL: https://admintools2.dol.gov/VETS100/Internal/Report/ExecuteSearch
                /*******************************************************************************************************************************************************************************
                 ** External: _search=false&nd=1432327903902&rows=10&page=1&sidx=&sord=asc&FilterCriteria.CompanyNumber=T140375&FilterCriteria.FilingCycle=2014&FilterCriteria.FormType=
                 ** Internal: _search=false&nd=1432327903502&rows=10&page=1&sidx=&sord=asc&ReportId=&FilingCycle=2014&FormType=VETS100A&SubmittedBy=&SubmittedDate=&CompanyName=&CompanyNumber=T140375&EIN=&DUNS=&LocationName=&LocationState=TN&PointOfContact=
                 ******************************************************************************************************************************************************************************/
                if (((pdfStateObject)state).InternalAccess)
                    inputData = string.Format("_search=false&nd={0}&rows={1}&page=1&sidx=&sord=asc&ReportId=&FilingCycle={2}&FormType=&SubmittedBy=&SubmittedDate=&CompanyName={3}&CompanyNumber=&EIN=&DUNS=&LocationName=&LocationState=TN&PointOfContact=",
                        nd.CreatePassword(), numRecords, ((pdfStateObject)state).FilingCycle, System.Web.HttpUtility.UrlEncode(((pdfStateObject)state).CompanyName));
                else
                    inputData = string.Format("_search=false&nd={0}&rows={1}&page=1&sidx=&sord=asc&FilterCriteria.CompanyNumber={2}&FilterCriteria.FilingCycle={3}&FilterCriteria.FormType=",
                        nd.CreatePassword(), numRecords, ((pdfStateObject)state).CompanyNumber, ((pdfStateObject)state).FilingCycle);

                // encode data as ASCII
                //ASCIIEncoding encoding = new ASCIIEncoding();
                encoding = System.Text.Encoding.UTF8;
                byteData = encoding.GetBytes(inputData);

                // set the content length
                request.ContentLength = byteData.Length;

                // get the request stream
                requestStream = request.GetRequestStream();
                requestStream.Write(byteData, 0, byteData.Length);

                // get the response
                response = request.GetResponse();
                if (request.HaveResponse)
                {
                    // get the response stream
                    responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;

                    // reading using StreamReader
                    System.IO.TextReader reader = new System.IO.StreamReader(responseStream, encode);
                    JSONData = reader.ReadToEnd();

                    OnMessage(this, new MessageEventArgs("Retrieved all requested records."));

                    // release resources
                    if (reader != null)
                    {
                        // close and dispose
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }

                    if (responseStream != null)
                    {
                        // close and dispose
                        responseStream.Close();
                        responseStream.Dispose();
                        responseStream = null;
                    }
                }
                if (JSONData == "") return (false);

                // process the returned records for ReportIDs
                // initial split on brace for major records
                records = JSONData.Split('{');
                foreach (string record in records)
                {
                    // split field/values
                    string[] data = record.Split(',');
                    foreach (string field in data)
                    {
                        // split field to determine field name
                        string[] values = field.Split(':');
                        if (values[0] == "\"ReportId\"")
                        {
                            // add report ID
                            ReportIDs.Add(values[1]);

                            // done with this record
                            break;
                        }
                    }
                }

                // have the data
                return (true);
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while getting report IDs from JSON with message: {0}", ex.Message)));

                // bad return
                return (false);
            }
        }
        /// <summary>
        /// Procedure to obtain report identifiers utilizing the DOL API
        /// data is based on the URL filter being used
        /// </summary>
        /// <param name="state">Contains all required information needed to access the DOL API</param>
        /// <returns></returns>
        private ArrayList getReportIDsFromDataDotGov(pdfStateObject state)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;

            // using an array to store and return the ReportIDs
            ArrayList ReportIDs = new ArrayList();

            try
            {
                // configure for the skip records
                int skip = 0;

                // keep getting each row of data until we have all requested.
                while ((ReportIDs.Count % 100) == 0)
                {
                    // set the skip amount
                    Uri url = new Uri(string.Format(((pdfStateObject)state).reportIDUrl, skip));

                    // get results of query
                    request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                    request.KeepAlive = true;

                    // get the response
                    response = request.GetResponse();
                    if (request.HaveResponse)
                    {
                        // display message retrieving
                        OnMessage(this, new MessageEventArgs(String.Format("Received {0} ReportID's", ReportIDs.Count)));

                        // get the response stream for reading
                        System.IO.Stream responseStream = response.GetResponseStream();
                        System.Text.Encoding encode = System.Text.Encoding.UTF8;

                        // reading using StreamReader
                        System.IO.StreamReader reader = new System.IO.StreamReader(responseStream, encode);
                        System.Xml.XmlDocument results = new System.Xml.XmlDocument();
                        results.Load(reader);

                        // go through the Xml document and get each of the report IDs
                        System.Xml.XmlNode root = results.DocumentElement;
                        if (root.HasChildNodes)
                            getReportIDsFromXML(root, ref ReportIDs);

                        // release resources
                        if (reader != null)
                        {                            // close and dispose
                            reader.Close();
                            reader.Dispose();
                            reader = null;
                        }

                        if (responseStream != null)
                        {
                            // close and dispose
                            responseStream.Close();
                            responseStream.Dispose();
                            responseStream = null;
                        }
                    }

                    // update skip
                    skip += 100;
                }

                // return report IDs
                return (ReportIDs);
            }
            catch (Exception ex)
            {
                // unable to det reportIDs
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while getting report IDs from DataDotGov with message: {0}", ex.Message)));

                // return report IDs
                return (ReportIDs);
            }
        }
        /// <summary>
        /// This procedure get the requested PDF report from the VETS-4212 website
        /// each report is added to the PdfSharp PDF document
        /// </summary>
        /// <param name="state">Contains all information needed to get the PDF report</param>
        /// <param name="report">Name of the report type</param>
        /// <param name="cookies">Contains session information needed by the VETS-4212 website</param>
        /// <param name="pdfDoc">Actual PDF document returned</param>
        /// <returns></returns>
        private bool getVets100PdfByReportID(pdfStateObject state, string report, ref System.Net.CookieContainer cookies, out PdfSharp.Pdf.PdfDocument pdfDoc)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;
            System.IO.MemoryStream pdf = null;
            System.IO.Stream responseStream = null;

            // set default value for pdfDoc
            pdfDoc = null;

            try
            {
                // create the URL for current report
                string url = string.Format(((pdfStateObject)state).pdfAddress, report);

                // perform the login
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                request.KeepAlive = true;
                request.CookieContainer = cookies;

                // get the response stream
                response = request.GetResponse();
                if (request.HaveResponse)
                {
                    // get the response stream
                    responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;

                    // use a memory stream to save PDF prior to adding to multi-PDF document
                    pdf = new System.IO.MemoryStream();

                    // reading using StreamReader
                    byte[] buffer = new byte[2048];
                    int bytes = responseStream.Read(buffer, 0, buffer.Length);
                    while (bytes > 0)
                    {
                        // add to PDF
                        pdf.Write(buffer, 0, bytes);

                        // get the next amount
                        bytes = responseStream.Read(buffer, 0, buffer.Length);
                    }

                    // using PdfSharp to combine documents
                    pdfDoc = PdfSharp.Pdf.IO.PdfReader.Open(pdf, PdfSharp.Pdf.IO.PdfDocumentOpenMode.Import);
                }

                // good result
                return (true);
            }
            catch (Exception ex)
            {
                // unable to det reportIDs
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while getting PDF by report ID with message: {0}", ex.Message)));
                System.Diagnostics.Debug.WriteLine(string.Format("Unable to get PDF from site due to: {0}", ex.Message));

                // no good
                return (false);
            }
            finally
            {
                if (pdf != null)
                {
                    // close and dispose
                    pdf.Close();
                    pdf.Dispose();
                    pdf = null;
                }

                if (responseStream != null)
                {
                    // close and dispose
                    responseStream.Close();
                    responseStream.Dispose();
                    responseStream = null;
                }

                if (response != null)
                {
                    // close and dispose
                    response.Close();
                    response = null;
                }
                if (request != null) request = null;
            }
        }
        /// <summary>
        /// Gets listing of all ReportIDs returned by calling "getReportIDsFromDataDotGov"
        /// </summary>
        /// <param name="node">Root XmlNode to start processing</param>
        /// <param name="ReportIDs">Array listing of all found ReportIDs</param>
        private void getReportIDsFromXML(System.Xml.XmlNode node, ref ArrayList ReportIDs)
        {
            try
            {
                foreach (System.Xml.XmlNode child in node.ChildNodes)
                {
                    // process any child nodes
                    if (child.HasChildNodes)
                        getReportIDsFromXML(child, ref ReportIDs);

                    // is this the node we are looking for d:ReportID
                    if (child.Name == "d:ReportID")
                        ReportIDs.Add(child.InnerText);
                }
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened while getting report IDs with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// Processes a single row from the VETS-4212 batch file, company information can be bypassed if evaluating test files
        /// </summary>
        /// <param name="record">This is the single delimited line of data from batch file</param>
        /// <param name="row">This is the current batch file row being processed</param>
        /// <param name="SCStates">This stores any state consolidated information for later processing</param>
        /// <param name="comments">This is a running log of actions during processing file</param>
        /// <param name="flatFileInformation">Contains information required to access VETS-4212 site and obtain company information for this company number</param>
        /// <param name="stats">Contains statistics of records being processed</param>
        /// <param name="companyInfo">Contains listing of each company information from current batch file</param>
        /// <returns></returns>
        private bool processVets4212Record(string record, int row, ref ArrayList SCStates, ref StringBuilder comments, vets4212StateObject flatFileInformation, ref vets4212ReportStats stats, ref List<CompanyInformation> companyInfo)
        {
            Regex re = null;
            object[] evalRecord = null, eval2Record = null;
            int totalValue;

            try
            {
                // split the record into columns
                string[] columns = record.Split(',');

                // do we have the correct number of columns
                if (columns.Length != _vets4212Record.Length)
                {
                    comments.AppendLine(string.Format("Invalid number of columns for row [{0}], should be {1:#0}, have {2:#0}", row, _vets4212Record.Length, columns.Length));
                    return (false);
                }

                // get appSetting bypassCompanyValidation
                bool bypassCompanyValidation = false;
                if (!bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["bypassCompanyValidation"], out bypassCompanyValidation))
                    bypassCompanyValidation = false;

                // get company information for this record
                System.Net.CookieContainer cookie = null;

                // get company information for comparison
                CompanyInformation cInfo = null;
                bool addCInfo = true;

                if (!bypassCompanyValidation)
                {
                    // first do we already have the record
                    foreach (CompanyInformation ci in companyInfo)
                    {
                        if (ci.CompanyNumber == columns[(int)_vets4212Fields.companyNumber])
                        {
                            // use current record
                            cInfo = ci;
                            addCInfo = false;
                            break;
                        }
                    }

                    if ((cInfo == null) && (!getCompanyInformation(flatFileInformation.Username, flatFileInformation.Password, flatFileInformation.CompanyInformationAddress,
                        flatFileInformation.WebAddress, flatFileInformation.InternalAccess, columns[(int)_vets4212Fields.companyNumber], out cInfo, out cookie)))
                    {
                        comments.AppendLine("Processing stopped due to: Unable to get company information");
                        return (false);
                    }

                    // do we need to add company record
                    if (addCInfo)
                        companyInfo.Add(cInfo);
                }

                // update statistics
                stats.TotalReports += 1;

                // get state and report type
                if ((columns[(int)_vets4212Fields.typeOfForm] == "MHQ") || (columns[(int)_vets4212Fields.typeOfForm] == "S"))
                {
                    // get state based on company information
                    string state = columns[(int)_vets4212Fields.parentCompanyState];

                    // add record if needed
                    if (!stats.Reports.ContainsKey(state))
                        stats.Reports.Add(state, new vets4212ReportStats.stateReports());

                    // update based on reportType
                    if (columns[(int)_vets4212Fields.typeOfForm] == "MHQ")
                        stats.Reports[state].MHQReports += 1;
                    else
                        stats.Reports[state].SReports += 1;
                }
                else
                {
                    // get state based on hiring location
                    string state = columns[(int)_vets4212Fields.hiringLocationState];

                    // add record if needed
                    if (!stats.Reports.ContainsKey(state))
                        stats.Reports.Add(state, new vets4212ReportStats.stateReports());

                    // update based on reportType
                    if (columns[(int)_vets4212Fields.typeOfForm] == "MHL")
                        stats.Reports[state].MHLReports += 1;
                    else
                        stats.Reports[state].MSCReports += 1;
                }

                if (!bypassCompanyValidation)
                {
                    // validate parent company information
                    if (string.Compare(cInfo.CompanyName, columns[(int)_vets4212Fields.parentCompanyName], true) != 0)
                        comments.AppendLine(string.Format("Parent company name does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyName], cInfo.CompanyName));
                    if (string.Compare(cInfo.Address, columns[(int)_vets4212Fields.parentCompanyStreet], true) != 0)
                        comments.AppendLine(string.Format("Parent street address does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyStreet], cInfo.Address));
                    if (string.Compare(cInfo.County, columns[(int)_vets4212Fields.parentCompanyCounty], true) != 0)
                        comments.AppendLine(string.Format("Parent county does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyCounty], cInfo.County));
                    if (string.Compare(cInfo.City, columns[(int)_vets4212Fields.parentCompanyCity], true) != 0)
                        comments.AppendLine(string.Format("Parent city does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyCity], cInfo.City));
                    if (string.Compare(cInfo.State, columns[(int)_vets4212Fields.parentCompanyState], true) != 0)
                        comments.AppendLine(string.Format("Parent state does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyState], cInfo.State));
                    if (string.Compare(cInfo.Zipcode, columns[(int)_vets4212Fields.parentCompanyZipcode], true) != 0)
                        comments.AppendLine(string.Format("Parent Zipcode does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.parentCompanyZipcode], cInfo.Zipcode));

                    // validate company contact information
                    string companyContact = string.Format("{0} {1}", cInfo.Firstname, cInfo.Lastname);
                    if (string.Compare(companyContact, columns[(int)_vets4212Fields.companyContact], true) != 0)
                        comments.AppendLine(string.Format("Parent contact does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.companyContact], companyContact));
                    if (string.Compare(cInfo.Phone, columns[(int)_vets4212Fields.companyContactTelephone], true) != 0)
                        comments.AppendLine(string.Format("Parent contact phone does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.companyContactTelephone], cInfo.Phone));
                    if (string.Compare(cInfo.Email, columns[(int)_vets4212Fields.compoanyContactEmail], true) != 0)
                        comments.AppendLine(string.Format("Parent contact email does not match row {0}; value: {1}, should be {2}", row, columns[(int)_vets4212Fields.compoanyContactEmail], cInfo.Email));
                }

                // go through each column and validate data
                for (int i = 0; i < _vets4212Record.Length; i++)
                {
                    // check validation if required
                    evalRecord = ((object[])_vets4212Record[i]);
                    if (((bool)(evalRecord[(int)_vets4212RecordColumns.required])) || (!string.IsNullOrEmpty(columns[i])))
                    {
                        // create the regular expression to evaluate the column
                        re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                        if (!re.IsMatch(columns[i]))
                        {
                            // add comment for this row and column
                            comments.AppendLine(string.Format("Row [{0:#,##0}] [{1}].", row, string.Format((string)evalRecord[(int)_vets4212RecordColumns.errorMessage], evalRecord[(int)_vets4212RecordColumns.name])));
                        }
                    }
                }

                // only allow hiring location when type of form is MHL or MSC
                if ((columns[(int)_vets4212Fields.typeOfForm] == "MHQ") || (columns[(int)_vets4212Fields.typeOfForm] == "S"))
                {
                    // should not have any hiring location information
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationName]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Name is not allowed for form type Headquarters or Single Establishment.", row));

                    // hiring location street
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationStreet]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Street is not allowed for form type Headquarters or Single Establishment.", row));

                    // hiring location city
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationCity]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location City is not allowed for form type Headquarters or Single Establishment.", row));

                    // hiring location county
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationCounty]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location County is not allowed for form type Headquarters or Single Establishment.", row));

                    // hiring location state
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationState]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location State is not allowed for form type Headquarters or Single Establishment.", row));

                    // hiring location zipcode
                    if (!string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationZipcode]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Zipcode is not allowed for form type Headquarters or Single Establishment.", row));
                }
                // should have hiring location information
                if ((columns[(int)_vets4212Fields.typeOfForm] == "MSC") || (columns[(int)_vets4212Fields.typeOfForm] == "MHL"))
                {
                    // hiring location name
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationName]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Name is required for form type Hiring Location or State Consolidated.", row));

                    // hiring location street
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationStreet]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Street is required for form type Hiring Location or State Consolidated.", row));

                    // hiring location city
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationCity]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location City is required for form type Hiring Location or State Consolidated.", row));

                    // hiring location county
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationCounty]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location County is required for form type Hiring Location or State Consolidated.", row));

                    // hiring location state
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationState]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location State is required for form type Hiring Location or State Consolidated.", row));

                    // hiring location zipcode
                    if (string.IsNullOrWhiteSpace(columns[(int)_vets4212Fields.hiringLocationZipcode]))
                        comments.AppendLine(string.Format("Row [{0:#,##0}] Hiring Location Zipcode is required for form type Hiring Location or State Consolidated.", row));
                }

                // check items based on type of reporting
                // if type of form MSC locations required
                if (columns[(int)_vets4212Fields.typeOfForm] == "MSC")
                {
                    int numLocations = 0;

                    // make sure that the number of locations exists
                    evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.numberOfMSCLocations]);
                    re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                    if (!re.IsMatch(columns[(int)_vets4212Fields.numberOfMSCLocations]))
                    {
                        // add coment for this row and column
                        comments.AppendLine(string.Format("Row [{0:#,##0}] {1} is required.", row, evalRecord[(int)_vets4212RecordColumns.name]));
                    }
                    else numLocations = int.Parse(columns[(int)_vets4212Fields.numberOfMSCLocations]);

                    // add current location state
                    evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationState]);
                    re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                    if (re.IsMatch(columns[(int)_vets4212Fields.hiringLocationState]))
                    {
                        // add the record
                        SCStates.Add(new object[] { columns[(int)_vets4212Fields.hiringLocationState], numLocations });
                    }
                }

                // process actual values to make sure they are correct
                int total = 0, overallTotal = 0;
                // make sure we have valid numbers
                for (int i = (int)_vets4212Fields.NumEmps_ProtectedVeterans1; i < (int)_vets4212Fields.NumEmps_ProtectedVeterans11; i++)
                {
                    int value, compareValue;
                    evalRecord = null; eval2Record = null;
                    if (!int.TryParse(columns[i], out value)) value = 0;
                    if (!int.TryParse(columns[i + 11], out compareValue)) compareValue = 0;

                    // add to the totals
                    total += value;
                    overallTotal += compareValue;

                    // determine if values are correct
                    evalRecord = ((object[])_vets4212Record[i]);
                    eval2Record = ((object[])_vets4212Record[i + 11]);
                    if (compareValue < value)
                        comments.AppendLine(string.Format("row [{0:#,##0}] [{1}].", row, string.Format((string)eval2Record[(int)_vets4212RecordColumns.errorMessage], eval2Record[(int)_vets4212RecordColumns.name])));
                }

                // set record names
                evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.NumEmps_ProtectedVeterans11]);

                // make sure 35 total is correct, sum 25 - 34
                if (!int.TryParse(columns[(int)_vets4212Fields.NumEmps_ProtectedVeterans11], out totalValue)) totalValue = 0;
                if (total < totalValue)
                    comments.AppendLine(string.Format("row [{0:#,##0}] [{1}].", row, string.Format((string)evalRecord[(int)_vets4212RecordColumns.errorMessage], evalRecord[(int)_vets4212RecordColumns.name])));

                // set record names
                evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.NumEmps_TotalAllVeteransNonVeterans11]);

                // make sure 46 overall total is correct, sum 36 - 45
                if (!int.TryParse(columns[(int)_vets4212Fields.NumEmps_TotalAllVeteransNonVeterans11], out totalValue)) totalValue = 0;
                if (overallTotal < totalValue)
                    comments.AppendLine(string.Format("row [{0:#,##0}] [{1}].", row, string.Format((string)evalRecord[(int)_vets4212RecordColumns.errorMessage], evalRecord[(int)_vets4212RecordColumns.name])));

                // reset totals
                total = 0;
                overallTotal = 0;

                // process All veterans & non-veterans
                for (int i = (int)_vets4212Fields.NewHire_ProtectedVeterans1; i < (int)_vets4212Fields.NewHire_ProtectedVeterans11; i++)
                {
                    int value, compareValue;
                    evalRecord = null; eval2Record = null;
                    if (!int.TryParse(columns[i], out value)) value = 0;
                    if (!int.TryParse(columns[i + 11], out compareValue)) compareValue = 0;

                    // determine if values are correct
                    evalRecord = ((object[])_vets4212Record[i]);
                    eval2Record = ((object[])_vets4212Record[i + 11]);
                    if (compareValue < value)
                        comments.AppendLine(string.Format("row [{0:#,##0}] [{1}].", row, string.Format((string)eval2Record[(int)_vets4212RecordColumns.errorMessage], eval2Record[(int)_vets4212RecordColumns.name])));
                }

                // make sure NewHire_TotalAllVeteransNonVeterans11 is greater than NewHire_ProtectedVeterans11
                if (!int.TryParse(columns[(int)_vets4212Fields.NewHire_ProtectedVeterans11], out totalValue)) totalValue = 0;
                if (!int.TryParse(columns[(int)_vets4212Fields.NewHire_TotalAllVeteransNonVeterans11], out overallTotal)) overallTotal = 0;
                if (overallTotal < totalValue)
                    comments.AppendLine(string.Format("row [{0:#,##0}]: The value for New Hire Total All Veterans and Non-Veterans must be greater than or equal to New Hire Total Protected Veterans.", row));

                // make sure maximum employees is greater than minimum employees
                int MaxValue, MinValue;
                if (!int.TryParse(columns[(int)_vets4212Fields.Maximum], out MaxValue)) MaxValue = 0;
                if (!int.TryParse(columns[(int)_vets4212Fields.Minimum], out MinValue)) MinValue = 0;
                if (MaxValue < MinValue)
                    comments.AppendLine(string.Format("row [{0:#,##0}]: The value for Maximum number of employees must be greater than Minimum number of employees", row));

                // good return
                return (true);
            }
            catch (Exception ex)
            {
                // handle exception
                comments.AppendLine(ex.Message);

                // return bad
                return (false);
            }
        }
        /// <summary>
        /// Processes the hiring locations file when state consolidated reports were part of the main batch file
        /// </summary>
        /// <param name="data">This contains all records from the hiring locations file</param>
        /// <param name="SCStates">This contains all teh state consolidated reports information from main batch file to be compared with this file</param>
        /// <param name="comments">This is the running log of events during processing</param>
        /// <param name="stats">Tshis contains the statistics during processing the hiring locations file</param>
        /// <returns></returns>
        private bool processVets4212HlRecords(string data, ArrayList SCStates, ref StringBuilder comments, ref vets4212ReportStats stats)
        {
            Regex re = null;
            object[] evalRecord = null;
            int row = 0;
            Dictionary<string, int> states = new Dictionary<string, int>();

            try
            {
                // add information showing moved to hiring locations file
                OnMessage(this, new MessageEventArgs("Processing State Consolidated Hiring Locations File..."));

                // local variables
                string line = string.Empty;

                // create a text reader
                TextReader reader = new StringReader(data);
                while ((line = reader.ReadLine()) != null)
                {
                    // make sure we have data on this line
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        // split the record into columns
                        string[] columns = line.Split(',');

                        // do we have the correct number of columns
                        if (columns.Length != _vets4212HlRecord.Length)
                        {
                            comments.AppendLine("Processing hiring locations file...");
                            comments.AppendLine(string.Format("Invalid number of columns, should be {0:#0}, have {1:#0}", _vets4212Record.Length, columns.Length));
                        }

                        // increment row counter
                        row++;

                        // go through each column and validate data
                        for (int i = 0; i < _vets4212HlRecord.Length; i++)
                        {
                            // check validation if required
                            evalRecord = ((object[])_vets4212HlRecord[i]);
                            if (((bool)(evalRecord[(int)_vets4212RecordColumns.required])) || (!string.IsNullOrEmpty(columns[i])))
                            {
                                // create the regular expression to evaluate the column
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                                if (!re.IsMatch(columns[i]))
                                {
                                    // add comment for this row and column
                                    comments.AppendLine(string.Format("Row [{0:#,##0}] [{1}].", row, string.Format((string)evalRecord[(int)_vets4212RecordColumns.errorMessage], evalRecord[(int)_vets4212RecordColumns.name])));
                                }
                            }
                        }

                        // update count of hiring locations 
                        string state = null;
                        if ((!string.IsNullOrWhiteSpace(columns[(int)_vets4212HiringLocationFields.LocationState])) &&
                            (states.ContainsKey(columns[(int)_vets4212HiringLocationFields.LocationState])))
                        {
                            state = columns[(int)_vets4212HiringLocationFields.LocationState];
                            states[state] += 1;
                        }
                        else
                        {
                            state = columns[(int)_vets4212HiringLocationFields.LocationState];
                            states.Add(state, 1);
                        }
                    }
                }

                // do we have more than one state consolidated report for same state
                Dictionary<string, int> stateConsolidatedReports = new Dictionary<string, int>();
                Dictionary<string, int> stateConsolidatedLocations = new Dictionary<string, int>();
                foreach (object item in SCStates)
                {
                    // get values for this record
                    string state = (string)((object[])item)[0];
                    int locations = (int)((object[])item)[1];

                    // add/update locations
                    if (stateConsolidatedLocations.ContainsKey(state))
                        stateConsolidatedLocations[state] = locations;
                    else
                        stateConsolidatedLocations.Add(state, locations);

                    // add/update dictionary
                    if (stateConsolidatedReports.ContainsKey(state))
                        stateConsolidatedReports[state] += 1;
                    else
                        stateConsolidatedReports.Add(state, 1);
                }
                // did we have any duplicate reports
                foreach (string state in stateConsolidatedReports.Keys)
                {
                    // if value is greater than 1 duplicate
                    if (stateConsolidatedReports[state] > 1)
                        comments.AppendLine(string.Format("Multiple State Consolidated Reports for [{0}], only last report will be used.", state.ToUpper()));
                }

                // did we have all the required locations
                foreach (string state in stateConsolidatedLocations.Keys)
                {
                    // does key exists in states
                    if (!states.ContainsKey(state))
                        comments.AppendLine(string.Format("Missing Hiring Locations for State Consolidated Report on [{0}]", state.ToUpper()));
                    else
                    {
                        if (states[state] != stateConsolidatedLocations[state])
                            comments.AppendLine(string.Format("Invalid number of Hiring Locations for State Consolidated Report on [{0}], should have been {1} Hiring Locations but found {2}.",
                                state.ToUpper(), states[state], stateConsolidatedLocations[state]));
                    }
                }

                // good return
                return (true);
            }
            catch (Exception ex)
            {
                // handle exception
                comments.AppendLine(ex.Message);

                // return bad
                return (false);
            }
        }
        /// <summary>
        /// This routine will get all report IDs from dataDotGov based on given criterea and then generate all the PDF files based on these report IDs
        /// this process is setup to be called as a user work queue
        /// </summary>
        /// <param name="ReportIDInformation">This object is TypeOf pdfStateObject and contains all information needed to perform this process</param>
        private void getReportIdsFromDataDotGov_QueueUserWorkItem(object ReportIDInformation)
        {
            // use a webclient to get each poll of data
            System.Net.CookieContainer cookies = null;

            try
            {
                // get report ID from data dot gov
                ArrayList ReportIDs = getReportIDsFromDataDotGov((pdfStateObject)ReportIDInformation);

                // setup the service point manager to handle bad certificates
                if (ServicePointManager.ServerCertificateValidationCallback == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    {
                        return (true);
                    };
                }

                // now we have all the required reportIDs, next step is to actually retrieve each of the reports.
                // attempt to login to VETS-100 system
                if (!loginToVets100Site((pdfStateObject)ReportIDInformation, out cookies)) return;

                // now we attempt to get each of the reports via PDF call

                // open the file for writing
                string fileName = System.IO.Path.GetFileNameWithoutExtension(((pdfStateObject)ReportIDInformation).Filename);
                string fileExt = System.IO.Path.GetExtension(((pdfStateObject)ReportIDInformation).Filename);
                string filePath = System.IO.Path.GetDirectoryName(((pdfStateObject)ReportIDInformation).Filename);

                // define current file
                int currentFileNumber = 1;
                string currentFileName = System.IO.Path.Combine(filePath, fileName);
                currentFileName = System.IO.Path.ChangeExtension(currentFileName, fileExt);

                System.IO.FileStream pdfFileStream = System.IO.File.Open(currentFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                PdfSharp.Pdf.PdfDocument pdfFile = new PdfSharp.Pdf.PdfDocument();
                pdfFile.Options.CompressContentStreams = true;

                // go through each report ID and request PDF
                foreach (string report in ReportIDs)
                {
                    // attempt to get the requested PDF and add to file
                    int count = 0;
                    PdfSharp.Pdf.PdfDocument pdfDoc = null;
                    while (count < 10)
                    {
                        if (getVets100PdfByReportID((pdfStateObject)ReportIDInformation, report, ref cookies, out pdfDoc))
                        {
                            // get the number of pages
                            int pages = pdfDoc.PageCount;

                            // add pages to PDF file
                            for (int idx = 0; idx < pages; idx++)
                            {
                                try
                                {
                                    // add page to file
                                    pdfFile.Pages.Add(pdfDoc.Pages[idx]);

                                    // update environment information
                                    OnEnvironment(this, new EnvironmentEventArgs(GC.GetTotalMemory(false), pdfFile.PageCount, pdfFileStream.Length));

                                    // if the file is loarger than 10MB start new file
                                    if (pdfFile.PageCount == 317)
                                    {
                                        // close the stream
                                        pdfFile.Save(pdfFileStream, false);
                                        pdfFileStream.Flush();
                                        pdfFileStream.Close();
                                        pdfFileStream.Dispose();

                                        // close file
                                        pdfFile.Close();
                                        pdfFile.Dispose();
                                        pdfFile = new PdfSharp.Pdf.PdfDocument();
                                        pdfFile.Options.CompressContentStreams = true;

                                        // new filename creation
                                        currentFileNumber++;
                                        currentFileName = System.IO.Path.Combine(filePath, string.Format("{0}_{1}", fileName, currentFileNumber));
                                        currentFileName = System.IO.Path.ChangeExtension(currentFileName, fileExt);
                                        pdfFileStream = System.IO.File.Open(currentFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                                    }
                                }
                                catch
                                {
                                    // get the current memory being used
                                    long preMemoryUsed = GC.GetTotalMemory(true);

                                    // wait for a short period
                                    OnMessage(this, new MessageEventArgs(string.Format("Waiting 5 minutes, current memory {0:#,##0}", preMemoryUsed)));
                                    Thread.Sleep(500000);

                                    // collect unused memory
                                    GC.Collect();

                                    // get the current meory being used
                                    long postMemoryUsed = GC.GetTotalMemory(true);
                                    OnMessage(this, new MessageEventArgs(string.Format("After collect current memory {0:#,##0}", preMemoryUsed)));

                                    // display message for continue
                                    OnMessage(this, new MessageEventArgs("Memory collection was performed due to out of memory condition."));

                                    // attempt to add the page
                                    pdfFile.Pages.Add(pdfDoc.Pages[idx]);
                                }
                            }

                            // report completed report
                            OnMessage(this, new MessageEventArgs(string.Format("Processed ReportID: {0}", report)));

                            // done processing break
                            break;
                        }
                        else
                        {
                            // relogin and request document

                            // clear cookies
                            cookies = null;

                            // let user know we are waiting
                            OnMessage(this, new MessageEventArgs("Waiting a minute before attempting to log back into the site."));

                            // sleep 1 minute
                            Thread.Sleep(60000);

                            // attempt login
                            if (!loginToVets100Site((pdfStateObject)ReportIDInformation, out cookies)) return;
                        }

                        // increment count
                        count++;
                    }

                    // release the PDF Document
                    if (pdfDoc != null)
                    {
                        pdfDoc.Clone();
                        pdfDoc.Dispose();
                        pdfDoc = null;
                    }
                }

                // close the file stream
                pdfFile.Save(pdfFileStream, false);
                pdfFileStream.Flush();
                pdfFileStream.Close();
                pdfFileStream.Dispose();

                // close the file
                pdfFile.Close();
                pdfFile.Dispose();

                // completed the process
                OnMessage(this, new MessageEventArgs(string.Format("Completed processing {0} reports...", ReportIDs.Count)));
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened during getting report IDs with message: {0}", ex.Message)));
            }
        }
       #endregion

        #region website interactions
        /*******************************************************************************************
         ** Application URL's
         **
         ** Internal URL's
         **     base URL https://admintools2.dol.gov/vets100/
         **     login URL https://admintools2.dol.gov/vets100/
         **     company Information URL https://admintools2.dol.gov/VETS100/Internal/Company/View/<companyNo>
         **
         ** External URL's
         **     base URL https://vets100.dol.gov/vets100/
         **     login URL https://vets100.dol.gov/vets100/
         **     company Information URL https://vets100.dol.gov/vets100/External/Company/View/<companyNo>
         ******************************************************************************************/
        private bool loginToVets100Site(pdfStateObject loginInformation, out System.Net.CookieContainer cookies)
        {
            try
            {
                vets4212Website websiteLogin = new vets4212Website(loginInformation.webAddress, loginInformation.Username, loginInformation.Password, loginInformation.InternalAccess);
                return (loginToVets100Site(websiteLogin, out cookies));
            }
            catch
            {
                throw;
            }
        }
        private bool loginToVets100Site(vets4212Website loginInformation, out System.Net.CookieContainer cookies)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;

            // create the cookie container for authentication
            cookies = new System.Net.CookieContainer();

            try
            {
                // first thing is to get the login page and authenticate the VETS-100 siste
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(loginInformation.Url);
                request.KeepAlive = true;
                request.CookieContainer = cookies;

                // we are looking for verification token
                string RequestVerificationToken = string.Empty;

                // get the response
                try
                {
                    response = request.GetResponse();
                }
                catch (Exception ex)
                {
                    OnMessage(this, new MessageEventArgs(ex.Message));
                    return (false);
                }
                if (request.HaveResponse)
                {
                    // get the response stream for reading
                    System.IO.Stream responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;

                    // reading using StreamReader
                    System.IO.TextReader reader = new System.IO.StreamReader(responseStream, encode);
                    string document = reader.ReadLine();

                    // go through the document and get the input field
                    while (!document.Contains("__RequestVerificationToken")) { document = reader.ReadLine(); }

                    // get the value of the verification token
                    int start = document.IndexOf("value=\"") + 7;
                    int end = document.IndexOf("\"", start);
                    RequestVerificationToken = document.Substring(start, end - start);

                    // release resources
                    if (reader != null)
                    {
                        // close and dispose
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }

                    if (responseStream != null)
                    {
                        // close and dispose
                        responseStream.Close();
                        responseStream.Dispose();
                        responseStream = null;
                    }
                }

                // perform the login
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(loginInformation.Url);
                request.KeepAlive = true;
                request.CookieContainer = cookies;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string inputData = string.Format("__RequestVerificationToken={0}&UserName={1}&Password={2}",
                    RequestVerificationToken, loginInformation.Username, System.Web.HttpUtility.UrlEncode(loginInformation.Password));

                // encode data as ASCII
                //ASCIIEncoding encoding = new ASCIIEncoding();
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                byte[] byteData = encoding.GetBytes(inputData);

                // set the content length
                request.ContentLength = byteData.Length;

                // get the request stream
                System.IO.Stream requestStream = request.GetRequestStream();
                requestStream.Write(byteData, 0, byteData.Length);

                // get the response
                response = request.GetResponse();
                if (request.HaveResponse)
                {
                    // get the response stream
                    System.IO.Stream responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;

                    // reading using StreamReader
                    System.IO.TextReader reader = new System.IO.StreamReader(responseStream, encode);
                    string document = reader.ReadToEnd();
                    if (!document.Contains(">Log off<"))
                    {
                        // display message of failure and return
                        OnMessage(this, new MessageEventArgs("Unable to login to VETS-100 system."));
                        return (false);
                    }

                    OnMessage(this, new MessageEventArgs("Logged into the VETS-100 system."));

                    // release resources
                    if (reader != null)
                    {
                        // close and dispose
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }

                    if (responseStream != null)
                    {
                        // close and dispose
                        responseStream.Close();
                        responseStream.Dispose();
                        responseStream = null;
                    }
                }

                // finally return logged in
                return (true);
            }
            catch (Exception ex)
            {
                // unable to det reportIDs
                OnMessage(this, new MessageEventArgs(string.Format("Unable to login to the VETS-100 site due to: {0}", ex.Message)));

                // no good login
                return (false);
            }
        }
        /// <summary>
        /// This function gets the full company information based on company number
        /// </summary>
        /// <param name="Username">Username to access VETS-4212 website</param>
        /// <param name="Password">Password to access VETS-4212 website</param>
        /// <param name="CompanyInformationAddress">Base URL to access VETS-4212 website</param>
        /// <param name="LoginAddress">URL used when authenticating to the VETS-4212 website</param>
        /// <param name="InternalAccess">Determines if on the ECN/DCN or using external network connection</param>
        /// <param name="CompanyNumber">The company number we all looking up the company information</param>
        /// <param name="companyInfo">Returned company information</param>
        /// <param name="cookies">Used for session and authentication on website</param>
        /// <returns></returns>
        private bool getCompanyInformation(string Username, string Password, string CompanyInformationAddress, string LoginAddress, bool InternalAccess, string CompanyNumber, out CompanyInformation companyInfo, out System.Net.CookieContainer cookies)
        {
            // use a webclient to get each poll of data
            System.Net.HttpWebRequest request = null;
            System.Net.WebResponse response = null;

            // create the cookie container for authentication
            cookies = new System.Net.CookieContainer();

            // create instance of company information
            companyInfo = new CompanyInformation();

            try
            {
                // make sure we have a valid company number format "T000000"
                Regex re = new Regex(@"^T[0-9]{6}$");
                if (!re.IsMatch(CompanyNumber))
                {
                    OnMessage(this, new MessageEventArgs(string.Format("[{0}] is not a valid company number.", CompanyNumber)));
                    return (false);
                }

                // create state object for login
                vets4212Website loginInformation = new vets4212Website(LoginAddress, Username, Password, InternalAccess);

                // attempt to login to VETS-100 system
                if (!loginToVets100Site(loginInformation, out cookies)) return (false);

                // first thing is to get the login page and authenticate the VETS-100 siste
                /***********************************************************************************************************
                 ** Extrnal: https://vets100.dol.gov/vets100/External/Company/View/<company number>
                 ** Internal https://vets100.dol.gov/vets100/Internal/Company/View/<company number>
                 **********************************************************************************************************/
                request = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(string.Format(CompanyInformationAddress, CompanyNumber.Substring(1)));

                // try to get the company information page
                request.KeepAlive = true;
                request.CookieContainer = cookies;

                // get the response
                response = request.GetResponse();
                if (request.HaveResponse)
                {
                    // get the response stream for reading
                    System.IO.Stream responseStream = response.GetResponseStream();
                    System.Text.Encoding encode = System.Text.Encoding.UTF8;
                    byte[] buffer = new byte[1024];
                    StringBuilder page = new StringBuilder();

                    // read all the content
                    int bytes = 0;
                    while ((bytes = responseStream.Read(buffer,0,buffer.Length)) > 0)
                    {
                        // add data to current page
                        page.Append(encode.GetString(buffer, 0, bytes));
                    }

                    // close the responseStream
                    if (responseStream != null)
                    {
                        // close and dispose
                        responseStream.Close();
                        responseStream.Dispose();
                        responseStream = null;
                    }

                    // Use HtmlDocument for parsing
                    HtmlAgilityPack.HtmlDocument document = new HtmlAgilityPack.HtmlDocument();
                    document.LoadHtml(page.ToString());

                    // get all the divs with company information displayed
                    List<HtmlAgilityPack.HtmlNode> divs = document.DocumentNode.Descendants().Where (
                        x => (x.Name == "div" && x.Attributes["class"] != null && x.Attributes["class"].Value.Contains("ym-fbox-text"))).ToList();

                    // get each of the company information values
                    string label = string.Empty;
                    string value = string.Empty;

                    foreach (HtmlAgilityPack.HtmlNode node in divs)
                    {
                        // first get the label for data
                        foreach (HtmlAgilityPack.HtmlNode child in node.ChildNodes)
                        {
                            // get label for this node
                            if ((child.Name == "p" || child.Name == "span") && child.Attributes["class"] != null && child.Attributes["class"].Value.Contains("dsp-label"))
                                label = child.InnerText;

                            if (child.Name == "div" && child.Attributes["class"] != null && child.Attributes["class"].Value.Contains("displaybox"))
                            {
                                foreach (HtmlAgilityPack.HtmlNode childNode in child.ChildNodes)
                                {
                                    if (childNode.Name == "span" && childNode.Attributes["class"] != null && childNode.Attributes["class"].Value.Contains("displaybox-text"))
                                        value = childNode.InnerText;
                                }
                            }
                        }

                        // based on the label set the correct value
                        switch (label)
                        {
                            case "Name:":
                                companyInfo.CompanyName = value;
                                break;

                            case "Company Number:":
                                companyInfo.CompanyNumber = value;
                                break;

                            case "EIN":
                                companyInfo.EIN = value;
                                break;

                            case "DUNS:":
                                companyInfo.DUNS = value;
                                break;

                            case "NAICS:":
                                companyInfo.NAICS = value;
                                break;

                            case "First Name:":
                                companyInfo.Firstname = value;
                                break;

                            case "Last Name:":
                                companyInfo.Lastname = value;
                                break;

                            case "Phone Number:":
                                companyInfo.Phone = value;
                                break;

                            case "Email Address:":
                                companyInfo.Email = value;
                                break;

                            case "Address:":
                                companyInfo.Address = value;
                                break;

                            case "County:":
                                companyInfo.County = value;
                                break;

                            case "City:":
                                companyInfo.City = value;
                                break;

                            case "State:":
                                companyInfo.State = value;
                                break;

                            case "Zip:":
                                companyInfo.Zipcode = value;
                                break;
                        }
                    }
                }

                // good return
                return (true);
            }
            catch (Exception ex)
            {
                // unable to det reportIDs
                OnMessage(this, new MessageEventArgs(string.Format("Unable to get company information due to: {0}", ex.Message)));

                // no good login
                return (false);
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// This is the public entry for getting specific dataDotGov information and is configured to be called uisng a thread queue
        /// </summary>
        /// <param name="DataDotGovInformation">This contains all the information needed and is TypeOf dataDotGovStateObject</param>
        public void getDataFromDataDotGov_QueueUserWorkItem(object DataDotGovInformation)
        {
            try
            {
                // we need to access dataDotGov to get the report IDs
                // URL format to use as follows: 
                //
                // http://api.dol.gov/v1/vets100/V100ADataDotGov?key=c91b753e-73c8-4efa-8964-eefcf4f4bffb&$filter=(FilingCycle eq 2014) and (CoName eq 'COX COM LLC')&$select=ReportID&$skip=0&$top=100
                //*********************************************************
                dataDotGovStateObject dataDotGovInfo = (dataDotGovStateObject)DataDotGovInformation;

                // create the formated URL
                string ReportType = string.Empty;
                switch (dataDotGovInfo.ReportType)
                {
                    case "VETS-100":
                        ReportType = "V100";
                        break;

                    case "VETS-100A":
                        ReportType = "V100A";
                        break;

                    case "VETS-4212":
                        ReportType = "V4212";
                        break;
                }

                // create the select statement: ReportID, CoName, HlName, NAICS
                string selectStatement = "ReportID";
                if (!string.IsNullOrWhiteSpace(dataDotGovInfo.CompanyName))
                    selectStatement += ",CoName ,HlName";
                if (!string.IsNullOrWhiteSpace(dataDotGovInfo.NAICS))
                    selectStatement += ",NAICS";

                // configure the query URL to be used with just reportType and FilingCycle
                //string reportIDUrl = string.Format("{0}{1}DataDotGov?key=c91b753e-73c8-4efa-8964-eefcf4f4bffb&$filter={2}&$select={3}&$top=100",
                //    dataDotGovInfo.DataDotGovAddress, ReportType, System.Web.HttpUtility.UrlEncode("FilingCycle eq 2014"),
                //    System.Web.HttpUtility.UrlEncode(selectStatement));
                string reportIDUrl = string.Format("{0}{1}DataDotGov?key=c91b753e-73c8-4efa-8964-eefcf4f4bffb&$filter={2}&$top=100",
                    dataDotGovInfo.DataDotGovAddress, ReportType, System.Web.HttpUtility.UrlEncode("FilingCycle eq 2014"));

                // add skip value
                reportIDUrl += "&$skip={0}";

                // time to get the data
                getReportDataFromDataDotGov(dataDotGovInfo.DataFilePath, ReportType, dataDotGovInfo.CompanyName, dataDotGovInfo.NAICS, reportIDUrl);

                // Completed getting report data
                OnDataDotGovCompleted(this, new DataDotGovEventArgs(string.Format("Completed getting data from DataDotGov, you data has been saved to: {0}", dataDotGovInfo.DataFilePath)));
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Unable to get data from DataDotGov due to: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// This is the public entry for getting PDF reports and is configured to be called using a thread queue
        /// </summary>
        /// <param name="ReportsInformation">This contains all the information needed and is TypeOf pdfStateObject</param>
        public void getReports_QueueUserWorkItem(object ReportsInformation)
        {
            // use a webclient to get each poll of data
            System.Net.CookieContainer cookies = null;
            ArrayList ReportIDs = null;

            try
            {

                // setup the service point manager to handle bad certificates
                if (ServicePointManager.ServerCertificateValidationCallback == null)
                {
                    ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
                    {
                        return (true);
                    };
                }

                // now we have all the required reportIDs, next step is to actually retrieve each of the reports.
                // attempt to login to VETS-100 system
                if (!loginToVets100Site((pdfStateObject)ReportsInformation, out cookies)) return;

                // now we need to perform a report query
                if (!getReportIDsFromJSON((pdfStateObject)ReportsInformation, ref cookies, out ReportIDs)) return;

                // get each of the PDF reports
                // open the file for writing
                string fileName = System.IO.Path.GetFileNameWithoutExtension(((pdfStateObject)ReportsInformation).Filename);
                string fileExt = System.IO.Path.GetExtension(((pdfStateObject)ReportsInformation).Filename);
                string filePath = System.IO.Path.GetDirectoryName(((pdfStateObject)ReportsInformation).Filename);

                // define current file
                int currentFileNumber = 1;
                string currentFileName = System.IO.Path.Combine(filePath, fileName);
                currentFileName = System.IO.Path.ChangeExtension(currentFileName, fileExt);

                System.IO.FileStream pdfFileStream = System.IO.File.Open(currentFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                //PdfSharp.Pdf.PdfDocument pdfFile = new PdfSharp.Pdf.PdfDocument(((pdfStateObject)state).Filename);
                PdfSharp.Pdf.PdfDocument pdfFile = new PdfSharp.Pdf.PdfDocument();
                pdfFile.Options.CompressContentStreams = true;

                // go through each report ID and request PDF
                foreach (string report in ReportIDs)
                {
                    // attempt to get the requested PDF and add to file
                    int count = 0;
                    PdfSharp.Pdf.PdfDocument pdfDoc = null;
                    while (count < 10)
                    {
                        if (getVets100PdfByReportID((pdfStateObject)ReportsInformation, report, ref cookies, out pdfDoc))
                        {
                            // get the number of pages
                            int pages = pdfDoc.PageCount;

                            // add pages to PDF file
                            for (int idx = 0; idx < pages; idx++)
                            {
                                try
                                {
                                    // add page to file
                                    pdfFile.Pages.Add(pdfDoc.Pages[idx]);

                                    // save to file stream
                                    //pdfFile.Save(pdfFileStream, false);

                                    // update environment information
                                    OnEnvironment(this, new EnvironmentEventArgs(GC.GetTotalMemory(false), pdfFile.PageCount, pdfFileStream.Length));

                                    // if the file is loarger than 10MB start new file
                                    if (pdfFile.PageCount == 317)
                                    {
                                        // close the stream
                                        pdfFile.Save(pdfFileStream, false);
                                        pdfFileStream.Flush();
                                        pdfFileStream.Close();
                                        pdfFileStream.Dispose();

                                        // close file
                                        pdfFile.Close();
                                        pdfFile.Dispose();
                                        pdfFile = new PdfSharp.Pdf.PdfDocument();
                                        pdfFile.Options.CompressContentStreams = true;

                                        // new filename creation
                                        currentFileNumber++;
                                        currentFileName = System.IO.Path.Combine(filePath, string.Format("{0}_{1}", fileName, currentFileNumber));
                                        currentFileName = System.IO.Path.ChangeExtension(currentFileName, fileExt);
                                        pdfFileStream = System.IO.File.Open(currentFileName, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
                                    }
                                }
                                catch
                                {
                                    // get the current memory being used
                                    long preMemoryUsed = GC.GetTotalMemory(true);

                                    // wait for a short period
                                    OnMessage(this, new MessageEventArgs(string.Format("Waiting 5 minutes, current memory {0:#,##0}", preMemoryUsed)));
                                    Thread.Sleep(500000);

                                    // collect unused memory
                                    GC.Collect();

                                    // get the current meory being used
                                    long postMemoryUsed = GC.GetTotalMemory(true);
                                    OnMessage(this, new MessageEventArgs(string.Format("After collect current memory {0:#,##0}", preMemoryUsed)));

                                    // display message for continue
                                    OnMessage(this, new MessageEventArgs("Memory collection was performed due to out of memory condition."));

                                    // attempt to add the page
                                    pdfFile.Pages.Add(pdfDoc.Pages[idx]);
                                }
                            }

                            // report completed report
                            OnMessage(this, new MessageEventArgs(string.Format("Processed ReportID: {0}", report)));

                            // done processing break
                            break;
                        }
                        else
                        {
                            // relogin and request document

                            // clear cookies
                            cookies = null;

                            // let user know we are waiting
                            OnMessage(this, new MessageEventArgs("Waiting a minute before attempting to log back into the site."));

                            // sleep 1 minute
                            Thread.Sleep(60000);

                            // attempt login
                            if (!loginToVets100Site((pdfStateObject)ReportsInformation, out cookies)) return;
                        }

                        // increment count
                        count++;
                    }

                    // release the PDF Document
                    if (pdfDoc != null)
                    {
                        pdfDoc.Clone();
                        pdfDoc.Dispose();
                        pdfDoc = null;
                    }
                }

                // close the file stream
                pdfFile.Save(pdfFileStream, false);
                pdfFileStream.Flush();
                pdfFileStream.Close();
                pdfFileStream.Dispose();

                // close the file
                pdfFile.Close();
                pdfFile.Dispose();

                // completed the process
                OnPdfReportsCompleted(this, new PDFEventArgs(ReportIDs.Count, string.Format("Completed processing {0} reports...", ReportIDs.Count)));
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception during generating reports with message: {0}", ex.Message)));
            }
            finally
            {
                // completed process
                OnMessage(this, new MessageEventArgs("Completed getting reports"));
            }
        }
        /// <summary>
        /// This is the public entry for fixing a given VETS-4212 batch file
        /// </summary>
        /// <param name="fixFlatFileInformation">This contains all the information needed and is TypeOf vets4212StateObject</param>
        public void fixFlatFile_QueueUserWorkItem(object fixFlatFileInformation)
        {
            try
            {
                // use for evaluating array of data
                vets4212StateObject fixFlatFileInfo = (vets4212StateObject)fixFlatFileInformation;
                object[] evalRecord = null, eval2Record = null;
                Regex re = null;

                // use a list of CompanyInformation
                List<CompanyInformation> companyInfo = new List<CompanyInformation>();

                // use stringbuilder for comments about file
                StringBuilder comments = new StringBuilder();

                // read in contents of file
                string data = System.IO.File.ReadAllText(((vets4212StateObject)fixFlatFileInformation).DataFilename);

                // use a reader to go line by line
                System.IO.TextReader reader = new System.IO.StringReader(data);
                string line = null;
                int row = 0;

                // configure for writing
                StringBuilder writer = new StringBuilder();

                while ((line = reader.ReadLine()) != null)
                {
                    // split the line into units
                    row++;
                    string[] columns = line.Split(',');

                    // update message for current row
                    OnMessage(this, new MessageEventArgs(string.Format("Processing data from row [{0:#,##0}]", row)));

                    // get appSetting bypassCompanyValidation
                    bool bypassCompanyValidation = false;
                    if (!bool.TryParse(System.Configuration.ConfigurationManager.AppSettings["bypassCompanyValidation"], out bypassCompanyValidation))
                        bypassCompanyValidation = false;

                    // get company information
                    System.Net.CookieContainer cookies = null;
                    bool valid = true;
                    bool addCompany = true;

                    // get company information for comparison
                    CompanyInformation cInfo = null;

                    if (bypassCompanyValidation)
                    {
                        // first do we already have the record
                        foreach (CompanyInformation ci in companyInfo)
                        {
                            if (ci.CompanyNumber == columns[(int)_vets4212Fields.companyNumber])
                            {
                                // use current record
                                cInfo = ci;
                                addCompany = false;
                                break;
                            }
                        }

                        if ((cInfo == null) && (!getCompanyInformation(fixFlatFileInfo.Username, fixFlatFileInfo.Password, fixFlatFileInfo.CompanyInformationAddress,
                            fixFlatFileInfo.WebAddress, fixFlatFileInfo.InternalAccess, columns[(int)_vets4212Fields.companyNumber], out cInfo, out cookies)))
                        {
                            comments.AppendLine("Processing stopped due to: Unable to get company information");
                            valid = false;
                        }

                        // add company to list if needed
                        if (addCompany)
                            companyInfo.Add(cInfo);
                    }

                    if (columns.Length != _vets4212Record.Length)
                    {
                        comments.AppendLine(string.Format("Invalid number of columns, should be {0}, but have {1:#0}", _vets4212Record.Length, columns.Length));
                        valid = false;
                    }

                    if (valid)
                    {
                        if (bypassCompanyValidation)
                        {
                            // correct any problems with parent company information
                            if (cInfo.CompanyName != columns[(int)_vets4212Fields.parentCompanyName])
                                columns[(int)_vets4212Fields.parentCompanyName] = cInfo.CompanyName;
                            if (cInfo.Address != columns[(int)_vets4212Fields.parentCompanyStreet])
                                columns[(int)_vets4212Fields.parentCompanyStreet] = cInfo.Address;
                            if (cInfo.County != columns[(int)_vets4212Fields.parentCompanyCounty])
                                columns[(int)_vets4212Fields.parentCompanyCounty] = cInfo.County;
                            if (cInfo.City != columns[(int)_vets4212Fields.parentCompanyCity])
                                columns[(int)_vets4212Fields.parentCompanyCity] = cInfo.City;
                            if (cInfo.State != columns[(int)_vets4212Fields.parentCompanyState])
                                columns[(int)_vets4212Fields.parentCompanyState] = cInfo.State;
                            if (cInfo.Zipcode != columns[(int)_vets4212Fields.parentCompanyZipcode])
                                columns[(int)_vets4212Fields.parentCompanyZipcode] = cInfo.Zipcode;

                            // correct any problems with contact information
                            string companyContact = string.Format("{0} {1}", cInfo.Firstname, cInfo.Lastname);
                            if (companyContact != columns[(int)_vets4212Fields.companyContact])
                                columns[(int)_vets4212Fields.companyContact] = string.Format("{0} {1}", cInfo.Firstname, cInfo.Lastname);
                            if (cInfo.Phone != columns[(int)_vets4212Fields.companyContactTelephone])
                                columns[(int)_vets4212Fields.companyContactTelephone] = cInfo.Phone;
                            if (cInfo.Email != columns[(int)_vets4212Fields.compoanyContactEmail])
                                columns[(int)_vets4212Fields.compoanyContactEmail] = cInfo.Email;
                        }

                        // correct any problems with DUNS, EIN, NAICS
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.DUNS]);
                        if (((bool)(evalRecord[(int)_vets4212RecordColumns.required])) || (!string.IsNullOrEmpty(columns[(int)_vets4212Fields.DUNS])))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.DUNS]))
                                columns[(int)_vets4212Fields.DUNS] = cInfo.DUNS;
                        }
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.FEIN]);
                        if (((bool)(evalRecord[(int)_vets4212RecordColumns.required])) || (!string.IsNullOrEmpty(columns[(int)_vets4212Fields.FEIN])))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.FEIN]))
                                columns[(int)_vets4212Fields.FEIN] = cInfo.EIN;
                        }
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.NAICS]);
                        if (((bool)(evalRecord[(int)_vets4212RecordColumns.required])) || (!string.IsNullOrEmpty(columns[(int)_vets4212Fields.NAICS])))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.NAICS]))
                                columns[(int)_vets4212Fields.NAICS] = cInfo.NAICS;
                        }

                        // look at hiring location information also based on type of report HQ nad S has no hiring location
                        // hiring location name
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationName]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationName]))
                            {
                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationName] = re.Replace(columns[(int)_vets4212Fields.hiringLocationName], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationName] = string.Empty;
                        }

                        // hiring location street
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationStreet]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationStreet]))
                            {
                                // replace any hash with Suite
                                columns[(int)_vets4212Fields.hiringLocationStreet] = columns[(int)_vets4212Fields.hiringLocationStreet].Replace("#", "Suite ");

                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationStreet] = re.Replace(columns[(int)_vets4212Fields.hiringLocationStreet], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationStreet] = string.Empty;
                        }

                        // hiring location street 2
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationStreet2]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationStreet2]))
                            {
                                // replace any hash with Suite
                                columns[(int)_vets4212Fields.hiringLocationStreet2] = columns[(int)_vets4212Fields.hiringLocationStreet2].Replace("#", "Suite ");

                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationStreet2] = re.Replace(columns[(int)_vets4212Fields.hiringLocationStreet2], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationStreet2] = string.Empty;
                        }

                        // hiring location county
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationCounty]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationCounty]))
                            {
                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationCounty] = re.Replace(columns[(int)_vets4212Fields.hiringLocationCounty], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationCounty] = string.Empty;
                        }

                        // hiring location city
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationCity]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationCity]))
                            {
                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationCity] = re.Replace(columns[(int)_vets4212Fields.hiringLocationCity], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationCity] = string.Empty;
                        }

                        // hiring location state
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationState]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationState]))
                            {
                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationState] = re.Replace(columns[(int)_vets4212Fields.hiringLocationState], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationState] = string.Empty;
                        }

                        // hiring location zipcode
                        evalRecord = ((object[])_vets4212Record[(int)_vets4212Fields.hiringLocationZipcode]);
                        if ((columns[(int)_vets4212Fields.typeOfForm] == "MHL") || (columns[(int)_vets4212Fields.typeOfForm] == "MSC"))
                        {
                            // create the regular expression to evaluate the column
                            re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.validation]);
                            if (!re.IsMatch(columns[(int)_vets4212Fields.hiringLocationZipcode]))
                            {
                                // clean string value
                                re = new Regex((string)evalRecord[(int)_vets4212RecordColumns.cleanStatement]);
                                columns[(int)_vets4212Fields.hiringLocationZipcode] = re.Replace(columns[(int)_vets4212Fields.hiringLocationZipcode], string.Empty);
                            }
                        }
                        else
                        {
                            // remove any hiring location information
                            columns[(int)_vets4212Fields.hiringLocationZipcode] = string.Empty;
                        }

                        // process actual values to make sure they are correct
                        int total = 0, overallTotal = 0, totalValue = 0;

                        // make sure we have valid numbers
                        for (int i = (int)_vets4212Fields.NumEmps_ProtectedVeterans1; i < (int)_vets4212Fields.NumEmps_ProtectedVeterans11; i++)
                        {
                            int value, compareValue;
                            evalRecord = null; eval2Record = null;
                            if (!int.TryParse(columns[i], out value)) value = 0;
                            if (!int.TryParse(columns[i + 11], out compareValue)) compareValue = 0;

                            // determine if values are correct
                            evalRecord = ((object[])_vets4212Record[i]);
                            eval2Record = ((object[])_vets4212Record[i + 11]);
                            if (compareValue < value)
                            {
                                // update column of data and value
                                columns[i + 11] = value.ToString();
                                compareValue = value;
                            }

                            // add to the totals
                            total += value;
                            overallTotal += compareValue;
                        }

                        // make sure 35 total is correct, sum 25 - 34
                        if (!int.TryParse(columns[(int)_vets4212Fields.NumEmps_ProtectedVeterans11], out totalValue)) totalValue = 0;
                        if (total != totalValue)
                            columns[(int)_vets4212Fields.NumEmps_ProtectedVeterans11] = total.ToString();

                        // make sure 46 overall total is correct, sum 36 - 45
                        if (!int.TryParse(columns[(int)_vets4212Fields.NumEmps_TotalAllVeteransNonVeterans11], out totalValue)) totalValue = 0;
                        if (overallTotal != totalValue)
                            columns[(int)_vets4212Fields.NumEmps_TotalAllVeteransNonVeterans11] = overallTotal.ToString();

                        // reset totals
                        total = 0;
                        overallTotal = 0;

                        // process All veterans & non-veterans
                        for (int i = (int)_vets4212Fields.NewHire_ProtectedVeterans1; i < (int)_vets4212Fields.NewHire_ProtectedVeterans11; i++)
                        {
                            int value, compareValue;
                            evalRecord = null; eval2Record = null;
                            if (!int.TryParse(columns[i], out value)) value = 0;
                            if (!int.TryParse(columns[i + 11], out compareValue)) compareValue = 0;

                            // determine if values are correct
                            evalRecord = ((object[])_vets4212Record[i]);
                            eval2Record = ((object[])_vets4212Record[i + 11]);
                            if (compareValue < value)
                            {
                                // update column of data and value
                                columns[i + 11] = value.ToString();
                                compareValue = value;
                            }
                        }

                        // make sure 68 NewHire_TotalAllVeteransNonVeterans11 is greater than 57 NewHire_ProtectedVeterans11
                        if (!int.TryParse(columns[(int)_vets4212Fields.NewHire_ProtectedVeterans11], out totalValue)) totalValue = 0;
                        if (!int.TryParse(columns[(int)_vets4212Fields.NewHire_TotalAllVeteransNonVeterans11], out overallTotal)) overallTotal = 0;
                        if (overallTotal < totalValue)
                            columns[(int)_vets4212Fields.NewHire_TotalAllVeteransNonVeterans11] = totalValue.ToString();

                        // write the line of data to writer
                        writer.AppendLine(string.Join(",", columns));
                    }
                }

                // write all back to file
                string filename = Path.Combine(Path.GetDirectoryName(fixFlatFileInfo.DataFilename), string.Format("Corrected_{0}", Path.GetFileName(fixFlatFileInfo.DataFilename)));
                System.IO.File.WriteAllText(filename, writer.ToString());

                // update message
                OnFixFlatFileCompleted(this, new FixFlatFileEventArgs(string.Format("Completed fixing file, corrected file: {0}", filename)));
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(string.Format("Exception happened during fixing flat file with message: {0}", ex.Message)));
            }
        }
        /// <summary>
        /// This is the public entry for evaluating a given VETS-4212 batch file
        /// </summary>
        /// <param name="flatFileInformation">This contains all the information needed and is TypeOf vets4212StateObject</param>
        public void evalFlatFile_QueueUserWorkItem(object flatFileInformation)
        {
            try
            {
                // we need to store statistics of files
                vets4212ReportStats stats = new vets4212ReportStats();

                // use a list of CompanyInformation
                List<CompanyInformation> companyInfo = new List<CompanyInformation>();

                // use arrayList to hold MSC state information
                ArrayList SCStates = new ArrayList();

                // use stringbuilder for comments about file
                StringBuilder comments = new StringBuilder();

                // add header for processing information
                OnMessage(this, new MessageEventArgs("Processing VETS-4212 report data file..."));

                // read in contents of file
                string data = System.IO.File.ReadAllText(((vets4212StateObject)flatFileInformation).DataFilename);

                // use a reader to go line by line
                System.IO.TextReader reader = new System.IO.StringReader(data);
                string line = null;
                int row = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    // split the line into units
                    row++;

                    // update message for current row
                    OnMessage(this, new MessageEventArgs(string.Format("Processing data from row [{0:#,##0}]", row)));

                    // process current row of data
                    if (!string.IsNullOrWhiteSpace(line))
                        processVets4212Record(line, row, ref SCStates, ref comments, (vets4212StateObject)flatFileInformation, ref stats, ref companyInfo);
                }

                // dispose of reader
                if (reader != null)
                    reader.Close();

                // make sure we have state consolidate file if reporst exists
                if ((string.IsNullOrWhiteSpace(((vets4212StateObject)flatFileInformation).HiringLocationDataFilename)) && (SCStates.Count > 0))
                    comments.AppendLine("State Consolidated Hiring Locations File is required when there are State Consolidated reports.");

                // do we have a state consolidated flat file for hiring locations
                if (!string.IsNullOrWhiteSpace(((vets4212StateObject)flatFileInformation).HiringLocationDataFilename))
                {
                    // get the contents of file
                    data = System.IO.File.ReadAllText(((vets4212StateObject)flatFileInformation).HiringLocationDataFilename);

                    // process current row of data
                    processVets4212HlRecords(data, SCStates, ref comments, ref stats);
                }

                // dispose of reader
                if (reader != null)
                    reader.Close();

                // determine if we found issues
                if (comments.ToString() != "")
                {
                    if (MessageBox.Show("Issues were found with flat file, save issues?", "SAVE ISSUES") == DialogResult.OK)
                    {
                        string filename = ((vets4212StateObject)flatFileInformation).DataFilename;
                        filename = System.IO.Path.ChangeExtension(filename, "log");
                        System.IO.File.WriteAllText(filename, comments.ToString());
                    }
                }

                // update message
                OnEvaluateFlatFileCompleted(this, new EvaluateFlatFileEventArgs(string.Format("Completed processing file: {0}", ((vets4212StateObject)flatFileInformation).DataFilename)));
            }
            catch (Exception ex)
            {
                OnMessage(this, new MessageEventArgs(ex.Message));
            }
        }
        #endregion
    }
}
