//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PFRandomData
{
    /// <summary>
    /// Basic prototype for a ProFast application or library class.
    /// </summary>
    public class RandomValues
    {
        //private work variables
        private StringBuilder _msg = new StringBuilder();

        //private variables for properties

        //enumerations
#pragma warning disable 1591
        public enum enNationalIdCountry
        {
            NotSpecified,
            Canada,
            Mexico,
            UnitedStates
        }
#pragma warning restore 1591

        //constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public RandomValues()
        {
            ;
        }

        //properties

        //methods

        /// <summary>
        /// Routine for generating the default national id of all zeros.
        /// </summary>
        /// <returns>Always returns string containing an all zeros national id.</returns>
        public string GetDefaultNationalId(enNationalIdCountry country)
        {
            string nationalId = "000000000";

            switch (country)
            {
                case enNationalIdCountry.UnitedStates:
                    nationalId = "000-00-0000";
                    break;
                case enNationalIdCountry.Canada:
                    nationalId = "000-000-000";
                    break;
                case enNationalIdCountry.Mexico:
                    nationalId = "XXXX000000XXXXXX00";
                    break;
                default:
                    nationalId = "000000000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <param name="country">Country for which the random id will be formatted.</param>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalId(enNationalIdCountry country)
        {
            string nationalId = "000000000";

            switch (country)
            {
                case enNationalIdCountry.UnitedStates:
                    nationalId = GetNationalIdUS();
                    break;
                case enNationalIdCountry.Canada:
                    nationalId = GetNationalIdCAN();
                    break;
                case enNationalIdCountry.Mexico:
                    nationalId = GetNationalIdMEX();
                    break;
                default:
                    nationalId = "000000000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) SSN.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalIdUS()
        {
            RandomNumber rn = new RandomNumber();
            string nationalId = "000-00-0000";
            int randomFormat = 0;

            randomFormat = rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = rn.GenerateRandomInt(1, 999).ToString("000") + "-00-0000";
                    break;
                case 2:
                    nationalId = "000-" + rn.GenerateRandomInt(1, 99).ToString("00") + "-0000";
                    break;
                case 3:
                    nationalId = "000-00-" + rn.GenerateRandomInt(1, 9999).ToString("0000");
                    break;
                default:
                    nationalId = "000-00-0000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random national id.</returns>
        public string GetNationalIdCAN()
        {
            RandomNumber rn = new RandomNumber();
            string nationalId = "000-000-000";
            int randomFormat = 0;

            randomFormat = rn.GenerateRandomInt(1, 3);

            switch (randomFormat)
            {
                case 1:
                    nationalId = "0" + rn.GenerateRandomInt(1, 99).ToString("00") + "-000-000";
                    break;
                case 2:
                    nationalId = "000-" + rn.GenerateRandomInt(1, 999).ToString("000") + "-000";
                    break;
                case 3:
                    nationalId = "000-000-" + rn.GenerateRandomInt(1, 999).ToString("000");
                    break;
                default:
                    nationalId = "000-000-000";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random (non-valid) national id.
        /// </summary>
        /// <returns>String containing the random SSN.</returns>
        public string GetNationalIdMEX()
        {
            RandomNumber rn = new RandomNumber();
            RandomString rs = new RandomString();
            string nationalId = "XXXX000000XXXXXX00";
            int randomFormat = 0;

            randomFormat = rn.GenerateRandomInt(1, 4);

            switch (randomFormat)
            {
                case 1:
                    nationalId = rs.GetStringUC(4) + "000000" + rs.GetStringUC(6) + "00";
                    break;
                case 2:
                    nationalId = "YYXX" + "000000" + rs.GetStringUC(6) + "00";
                    break;
                case 3:
                    nationalId = "ZZXX" + "000000" + rs.GetStringUC(6) + "00";
                    break;
                case 4:
                    nationalId = "XXXX" + "000000" + rs.GetStringUC(6) + "00";
                    break;
                default:
                    nationalId = "XXXX000000XXXXXX00";
                    break;
            }

            return nationalId;
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 and area code of 000 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber()
        {
            return GetTelephoneNumber("1", "000");
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Country code of 1 will be specified in the resulting random telephone number.</remarks>
        public string GetTelephoneNumber(string areaCode)
        {
            return GetTelephoneNumber("1", areaCode);
        }

        /// <summary>
        /// Routine to generate a random and invalid telephone number.
        /// </summary>
        /// <param name="countryCode">Specify the country code for the telephone number.</param>
        /// <param name="areaCode">Specify the area code for the telephone number.</param>
        /// <returns>String containing random telephone number.</returns>
        /// <remarks>Leave countryCode and/or areaCode blank to omit those portions of the telephone number from the random result.</remarks>
        public string GetTelephoneNumber(string countryCode, string areaCode)
        {
            string telNo = "1-000-000-0000";
            StringBuilder sb = new StringBuilder();
            RandomNumber rn = new RandomNumber();

            if (countryCode.Length > 0)
            {
                sb.Append(countryCode);
                sb.Append("-");
            }
            if (areaCode.Length > 0)
            {
                sb.Append(areaCode);
                sb.Append("-");
            }

            sb.Append("555-");
            sb.Append(rn.GenerateRandomInt(100,199).ToString("0000"));

            telNo = sb.ToString();

            return telNo;
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress()
        {
            RandomNumber rn = new RandomNumber();
            string topLevelDomain = "000";

            switch (rn.GenerateRandomInt(1, 10))
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    topLevelDomain = "com";
                    break;
                case 5:
                case 6:
                case 7:
                    topLevelDomain = "net";
                    break;
                case 8:
                case 9:
                    topLevelDomain = "org";
                    break;
                case 10:
                    topLevelDomain = "edu";
                    break;
                default:
                    topLevelDomain = "000";
                    break;
            }

            return GetEmailAddress(topLevelDomain);
        }

        /// <summary>
        /// Routine to generate a random email address.
        /// </summary>
        /// <param name="topLevelDomain">Top level domain to use for this address (e.g. com, net, org, edu).</param>
        /// <returns>String containing random email address.</returns>
        /// <remarks>All email addresses are for the example domain.</remarks>
        public string GetEmailAddress(string topLevelDomain)
        {
            RandomString rs = new RandomString();
            RandomNumber rn = new RandomNumber();
            StringBuilder sb = new StringBuilder();
            string emailAddress = string.Empty;


            sb.Append(rs.GetRandomSyllablesLC(rn.GenerateRandomInt(2, 4)));
            sb.Append(@"@example.");
            sb.Append(topLevelDomain.TrimStart('.'));

            emailAddress = sb.ToString();

            return emailAddress;
        }


    }//end class
}//end namespace
