using System;
using System.Collections.Generic;
using BussinessObject.EventModule.Enum;

namespace BussinessObject.EventModule.Models
{
    public class LuckyEverydayViewModel
    {
        public DateTime EventDate { get; set; }
        public List<LuckyEverydayDateViewModel> LuckyEverydayDateViewModel { get; set; }

        public LuckyEverydayViewModel()
        {
            EventDate = DateTime.Now;
            LuckyEverydayDateViewModel = new List<LuckyEverydayDateViewModel>();
        }
    }

    public class LuckyEverydayDateViewModel
    {
        public LuckyEverydayPeriodTime LuckyEverydayPeriodTime { get; set; }
        public string PeriodTimeValue { get; set; }
        public List<LuckyEverydayPeriodTimeViewModel> LuckyEverydayPeriodTimeViewModel { get; set; }
        public LuckyEverydayDateViewModel()
        {
            LuckyEverydayPeriodTime = new LuckyEverydayPeriodTime();
            PeriodTimeValue = string.Empty;
            LuckyEverydayPeriodTimeViewModel = new List<LuckyEverydayPeriodTimeViewModel>();
        }
    }

    public class LuckyEverydayPeriodTimeViewModel
    {
        public string DisplayName { get; set; }
        public string Present { get; set; }
        public decimal Value { get; set; }
        public LuckyEverydayPeriodTimeViewModel()
        {
            DisplayName = string.Empty;
            Present = string.Empty;
            Value = 0M;
        }
    }
}