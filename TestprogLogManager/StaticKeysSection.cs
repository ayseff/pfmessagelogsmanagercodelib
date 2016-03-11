//****************************************************************************************************
//
// Copyright © ProFast Computing 2012-2015
//
//****************************************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TestprogLogManager
{
    public class StaticKeysSection : ConfigurationSection
    {
        private static StaticKeysSection settings = ConfigurationManager.GetSection("StaticKeysSection") as StaticKeysSection;

        public static StaticKeysSection Settings
        {
            get
            {
                return settings;
            }
        }

        [ConfigurationProperty("MainFormCaption", IsRequired = true, DefaultValue = "Application Options")]
        [StringValidator(InvalidCharacters = "~!@#$%^&*()[]{}/;’\"|\\"
          , MinLength = 0
          , MaxLength = 1024)]
        public string MainFormCaption
        {
            get { return (string)this["MainFormCaption"]; }
            set { this["MainFormCaption"] = value; }
        }

        [ConfigurationProperty("MinAppThreads", DefaultValue = 1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 25)]
        public int MinAppThreads
        {
            get { return (int)this["MinAppThreads"]; }
            set { this["MinAppThreads"] = value; }
        }

        [ConfigurationProperty("MaxAppThreads", DefaultValue = 1, IsRequired = false)]
        [IntegerValidator(MinValue = 1, MaxValue = 200)]
        public int MaxAppThreads
        {
            get { return (int)this["MaxAppThreads"]; }
            set { this["MaxAppThreads"] = value; }
        }

        [ConfigurationProperty("RequireLogon", DefaultValue = false, IsRequired = false)]
        public bool RequireLogon
        {
            get { return (bool)this["RequireLogon"]; }
            set
            {
                this["RequireLogon"] = value;
            }
        }


        [ConfigurationProperty("ValidBooleanValues", IsRequired = true, DefaultValue = "Application Options")]
        public string ValidBooleanValues
        {
            get { return (string)this["ValidBooleanValues"]; }
            set { this["ValidBooleanValues"] = value; }
        }


    }//end class
}//end namespace
