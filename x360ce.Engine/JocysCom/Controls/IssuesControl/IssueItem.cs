﻿using System;
using System.ComponentModel;

namespace JocysCom.ClassLibrary.Controls.IssuesControl
{
    public class IssueItem : INotifyPropertyChanged
    {

        public IssueItem()
        {
            Description = "";
            FixName = "Fix";
        }

        public event EventHandler<EventArgs> Checking;
        public event EventHandler<EventArgs> Checked;

        public event EventHandler<EventArgs> Fixing;
        public event EventHandler<EventArgs> Fixed;

        public IssueStatus Status { get { return _Status; } }
        IssueStatus _Status;

        void SetStatus(IssueStatus status)
        {
            _Status = status;
            NotifyPropertyChanged("Status");
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; NotifyPropertyChanged("Name"); }
        }
        string _Name;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; NotifyPropertyChanged("Description"); }
        }
        string _Description;

        public int FixType;

        public string FixName
        {
            get { return _FixName; }
            set { _FixName = value; NotifyPropertyChanged("FixName"); }
        }
        string _FixName;

        public IssueSeverity? Severity
        {
            get { return _Severity; }
            set { _Severity = value; NotifyPropertyChanged("Severity"); }
        }
        IssueSeverity? _Severity;

        public virtual void CheckTask()
        {
            throw new NotImplementedException();
        }

        public void Check()
        {
            SetStatus(IssueStatus.Checking);
            var ev1 = Checking;
            if (ev1 != null)
                ev1(this, new EventArgs());
            CheckTask();
            SetStatus(IssueStatus.Idle);
            var ev2 = Checked;
            if (ev2 != null)
                ev2(this, new EventArgs());
        }

        public virtual void FixTask()
        {
            throw new NotImplementedException();
        }

        public void Fix()
        {
            SetStatus(IssueStatus.Fixing);
            var ev1 = Fixing;
            if (ev1 != null)
                ev1(this, new EventArgs());
            FixTask();
            SetStatus(IssueStatus.Idle);
            var ev2 = Fixed;
            if (ev2 != null)
                ev2(this, new EventArgs());
        }

        public void SetSeverity(IssueSeverity severity, int fixType = 0, string description = "")
        {
            var update = !Severity.HasValue || Severity.Value != severity || Description != description;
            if (update)
            {
                FixType = fixType;
                Severity = severity;
                Description = description;
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName = "")
        {
            var ev = PropertyChanged;
            if (ev == null) return;
            ev(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }
}
