﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MVVMBase;


// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/
// https://docs.microsoft.com/xamarin/xamarin-forms/user-interface/listview/interactivity
// https://docs.microsoft.com/dotnet/api/xamarin.forms.listview?view=xamarin-forms

namespace SimpleListView
{
    public class MainPageViewModel : ViewModelBase
    {
        //**********************  PRIVATE MEMBER VARIABLES *********************

        private IMainPageHelper _viewHelper;
        private ObservableCollection<string> _planets; // Implements INotifyCollectionChanged
        private bool _selectionModeOn = true;
        private string _titleString = "Nothing Selected";
        private string _selectedString = "Nothing Selected";
        private int _selectedRow = 0;
        private int _selectionCount = 0;
        private int _tapCount = 0;

        // ***********************  BINDABLE PROPERTIES ************************

        // ItemSource binds to an IEnumerable
        public ObservableCollection<string> Planets
        {
            get => _planets;
            set
            {
                if (_planets == value) return;
                _planets = value;
                OnPropertyChanged();
            }
        }

        public bool SelectionModeOn {
            get => _selectionModeOn;
            set
            {
                if (_selectionModeOn == value) return;

                _selectionModeOn = value;
                OnPropertyChanged();
            }
        }

        public string TitleString {
            get => _titleString;
            set
            {
                if (_titleString == value) return;
                _titleString = value;
                OnPropertyChanged();
            }
        }

        //This property is updated if the ListView selection changes by any means but ONLY if the selection changes
        public string SelectedString
        {
            get => _selectedString;
            set
            {
                if (_selectedString == value) return;

                _selectedString = value;

                //Update UI
                TitleString = _selectedString ?? "Nothing Selected";
            }
        }

        public int SelectedRow
        {
            get => _selectedRow;
            set
            {
                if (_selectedRow == value) return;
                _selectedRow = value;
                OnPropertyChanged();
            }
        }

        public int SelectionCount {
            get => _selectionCount;
            set
            {
                if (_selectionCount == value) return;
                _selectionCount = value;
                OnPropertyChanged(nameof(CounterString));
            }
        }

        public int TapCount {
            get => _tapCount;
            set
            {
                if (_tapCount == value) return;
                _tapCount = value;
                OnPropertyChanged(nameof(CounterString));
            }
        }

        public string CounterString => $"Taps: {TapCount}, Selections: {SelectionCount}";

        // **************************  EVENT HANDLERS **************************

        //Event handler for user tap
        public void UserTappedList(int row, string planetString)
        {
            SelectedRow = row;
            TapCount += 1;
            string item = $"Row {row} tapped";
            Planets.Add(item);
            _viewHelper.ScrollToObject(item);
        }

        //Event handler for selection changed
        public void ItemSelectionChanged(int row, string planetString)
        {
            SelectedRow = row;
            SelectionCount += 1;
        }

        // ***************************  CONSTRUCTOR ****************************
        public MainPageViewModel(IMainPageHelper viewHelper) : base(viewHelper.NavigationProxy)
        {
            _viewHelper = viewHelper;

            Planets = new ObservableCollection<string>
            {
                "Mercury",
                "Venus",
                "Jupiter",
                "Earth",
                "Mars",
                "Saturn",
                "Pluto"
            };
        }

    } //END OF CLASS
} //END OF NAMESPACE
