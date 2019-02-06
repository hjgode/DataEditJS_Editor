﻿// <auto-generated />

namespace JSLintNet.UI.Settings
{
    internal partial class SettingsViewModel : ViewModelBase
    {
        public virtual bool RunOnSave
        {
            get
            {
                return this.Model.RunOnSave.GetValueOrDefault();
            }

            set
            {
                this.Model.RunOnSave = value ? (bool?)true : null;

                this.RaisePropertyChanged("RunOnSave");
            }
        }

        public virtual bool RunOnBuild
        {
            get
            {
                return this.Model.RunOnBuild.GetValueOrDefault();
            }

            set
            {
                this.Model.RunOnBuild = value ? (bool?)true : null;

                this.RaisePropertyChanged("RunOnBuild");
            }
        }

        public virtual bool CancelBuild
        {
            get
            {
                return this.Model.CancelBuild.GetValueOrDefault();
            }

            set
            {
                this.Model.CancelBuild = value ? (bool?)true : null;

                this.RaisePropertyChanged("CancelBuild");
            }
        }

        public virtual string ErrorLimit
        {
            get
            {
                var nullable = this.Model.ErrorLimit;

                return nullable.HasValue ? nullable.ToString() : null;
            }

            set
            {
                this.Model.ErrorLimit = ParseIntFromString(value);

                this.RaisePropertyChanged("ErrorLimit");
            }
        }

        public virtual string FileLimit
        {
            get
            {
                var nullable = this.Model.FileLimit;

                return nullable.HasValue ? nullable.ToString() : null;
            }

            set
            {
                this.Model.FileLimit = ParseIntFromString(value);

                this.RaisePropertyChanged("FileLimit");
            }
        }

        public virtual bool TolerateBitwiseOperators
        {
            get
            {
                return this.Model.Options.TolerateBitwiseOperators.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateBitwiseOperators = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateBitwiseOperators");
            }
        }

        public virtual bool AssumeBrowser
        {
            get
            {
                return this.Model.Options.AssumeBrowser.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.AssumeBrowser = value ? (bool?)true : null;

                this.RaisePropertyChanged("AssumeBrowser");
            }
        }

        public virtual bool AssumeCouchDB
        {
            get
            {
                return this.Model.Options.AssumeCouchDB.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.AssumeCouchDB = value ? (bool?)true : null;

                this.RaisePropertyChanged("AssumeCouchDB");
            }
        }

        public virtual bool AssumeInDevelopment
        {
            get
            {
                return this.Model.Options.AssumeInDevelopment.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.AssumeInDevelopment = value ? (bool?)true : null;

                this.RaisePropertyChanged("AssumeInDevelopment");
            }
        }

        public virtual bool AssumeES6
        {
            get
            {
                return this.Model.Options.AssumeES6.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.AssumeES6 = value ? (bool?)true : null;

                this.RaisePropertyChanged("AssumeES6");
            }
        }

        public virtual bool TolerateEval
        {
            get
            {
                return this.Model.Options.TolerateEval.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateEval = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateEval");
            }
        }

        public virtual bool TolerateForStatement
        {
            get
            {
                return this.Model.Options.TolerateForStatement.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateForStatement = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateForStatement");
            }
        }

        public virtual string MaximumErrors
        {
            get
            {
                var nullable = this.Model.Options.MaximumErrors;

                return nullable.HasValue ? nullable.ToString() : null;
            }

            set
            {
                this.Model.Options.MaximumErrors = ParseIntFromString(value);

                this.RaisePropertyChanged("MaximumErrors");
            }
        }

        public virtual string MaximumLineLength
        {
            get
            {
                var nullable = this.Model.Options.MaximumLineLength;

                return nullable.HasValue ? nullable.ToString() : null;
            }

            set
            {
                this.Model.Options.MaximumLineLength = ParseIntFromString(value);

                this.RaisePropertyChanged("MaximumLineLength");
            }
        }

        public virtual bool TolerateMultipleVariables
        {
            get
            {
                return this.Model.Options.TolerateMultipleVariables.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateMultipleVariables = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateMultipleVariables");
            }
        }

        public virtual bool AssumeNode
        {
            get
            {
                return this.Model.Options.AssumeNode.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.AssumeNode = value ? (bool?)true : null;

                this.RaisePropertyChanged("AssumeNode");
            }
        }

        public virtual bool TolerateSingleQuoteStrings
        {
            get
            {
                return this.Model.Options.TolerateSingleQuoteStrings.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateSingleQuoteStrings = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateSingleQuoteStrings");
            }
        }

        public virtual bool TolerateThis
        {
            get
            {
                return this.Model.Options.TolerateThis.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateThis = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateThis");
            }
        }

        public virtual bool TolerateMessyWhitespace
        {
            get
            {
                return this.Model.Options.TolerateMessyWhitespace.GetValueOrDefault();
            }

            set
            {
                this.Model.Options.TolerateMessyWhitespace = value ? (bool?)true : null;

                this.RaisePropertyChanged("TolerateMessyWhitespace");
            }
        }
    }
}