﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using JetBrains.Annotations;

namespace ClubJumana.Core.Convertors
{
    public class CostCenter : INotifyPropertyChanged
    {
        public CostCenter()
        {

        }

        private decimal _fobPrice = -1;

        public decimal FobPrice
        {
            get => _fobPrice;
            set
            {
                _fobPrice = value;

                //_fobPrice = value;
                //_landedCostUSD = _fobPrice * (1 + (LandedCostRate / 100));
                //_wholesaleUSD = _landedCostUSD * 1.55m;
                //_sales5USD = _wholesaleUSD * 0.05m;
                //_creditIN10USD = _wholesaleUSD * 0.1m + _sales5USD;
                //_wholeCashUSD = _sales5USD * 0.05m + _wholesaleUSD;
                //_wholeCreditUSD = _wholeCashUSD * 1.1m;
                //_wLGrossUSD = _wholeCashUSD - _landedCostUSD;
                //_wLMarginUSD = _wLGrossUSD / _wholeCashUSD * 100;
                //_retailUSD = _landedCostUSD * 4;
                //_rtlGrossUSD = (_retailUSD - _landedCostUSD) / _retailUSD * 100;

                if (_fobPrice == 0 || _landedCostRate == 0 || _exChangeRate == 1)
                {

                }
                else
                {
                   Calculate();
                }



            }
        }

        private decimal _landedCostRate = 0;
        public decimal LandedCostRate
        {
            get => _landedCostRate;
            set
            {
                _landedCostRate = value;


               Calculate();


            }
        }

        private decimal _exChangeRate = 1;

        public decimal ExchangeRate
        {
            get => _exChangeRate;
            set
            {
                _exChangeRate = value;


                Calculate();



            }
        }




        private decimal _landedCostUSD ;
        public decimal LandedCostUSD { get => _landedCostUSD; } 

        private decimal _wholesaleUSD = 0;
        public decimal WholesaleUSD { get => _wholesaleUSD; }

        private decimal _sales5USD = 0;
        public decimal Sales5USD { get => _sales5USD; }

        private decimal _creditIN10USD = 0;
        public decimal CreditIN10USD { get => _creditIN10USD; }

        private decimal _wholeCashUSD = 0;
        public decimal WholeCashUSD { get => _wholeCashUSD; }

        private decimal _wholeCreditUSD = 0;
        public decimal WholeCreditUSD { get => _wholeCreditUSD; }

        private decimal _wLGrossUSD = 0;
        public decimal WLGrossUSD { get => _wLGrossUSD; }

        private decimal _wLMarginUSD = 0;
        public decimal WLMarginUSD { get => _wLMarginUSD; }

        private decimal _retailUSD = 0;
        public decimal RetailUSD { get => _retailUSD; }

        private decimal _rtlGrossUSD = 0;
        public decimal RTLGrossUSD { get => _rtlGrossUSD; }


        private decimal _landedCostD = 0;
        public decimal LandedCostD { get => _landedCostD; }

        private decimal _wholesaleD = 0;
        public decimal WholesaleD { get => _wholesaleD; }

        private decimal _sales5D = 0;
        public decimal Sales5D { get => _sales5D; }

        private decimal _creditIN10D = 0;
        public decimal CreditIN10D { get => _creditIN10D; }

        private decimal _wholeCashD = 0;
        public decimal WholeCashD { get => _wholeCashD; }

        private decimal _wholeCreditD = 0;
        public decimal WholeCreditD { get => _wholeCreditD; }

        private decimal _wLGrossD = 0;
        public decimal WLGrossD { get => _wLGrossD; }

        private decimal _wLMarginD = 0;
        public decimal WLMarginD { get => _wLMarginD; }

        private decimal _retailD = 0;
        public decimal RetailD { get => _retailD; }

        private decimal _rtlGrossD = 0;
        public decimal RTLGrossD { get => _rtlGrossD; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private void Calculate()
        {
            _landedCostUSD = Math.Round((_fobPrice * (1 + (LandedCostRate / 100)) * 1.05m), 2, MidpointRounding.AwayFromZero);
            _wholesaleUSD = Math.Round(_landedCostUSD * 1.55m, 2, MidpointRounding.AwayFromZero);
            _sales5USD = Math.Round(_wholesaleUSD * 0.05m, 2, MidpointRounding.AwayFromZero);
            _creditIN10USD = Math.Round(_wholesaleUSD * 0.1m + _sales5USD, 2, MidpointRounding.AwayFromZero);
            _wholeCashUSD = Math.Round(_sales5USD * 0.05m + _wholesaleUSD, 2, MidpointRounding.AwayFromZero);
            _wholeCreditUSD = Math.Round(_wholeCashUSD * 1.1m, 2, MidpointRounding.AwayFromZero);
            _wLGrossUSD = Math.Round(_wholeCashUSD - _landedCostUSD, 2, MidpointRounding.AwayFromZero);
            _wLMarginUSD = Math.Round(_wLGrossUSD / _wholeCashUSD * 100, 2, MidpointRounding.AwayFromZero);
            _retailUSD = Math.Round(_landedCostUSD * 4, 2, MidpointRounding.AwayFromZero);
            _rtlGrossUSD = Math.Round((_retailUSD - _landedCostUSD) / _retailUSD * 100, 2, MidpointRounding.AwayFromZero);


            _landedCostD = Math.Round(_landedCostUSD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _wholesaleD = Math.Round(_wholesaleUSD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _sales5D = Math.Round(_sales5D * 0.05m, 2, MidpointRounding.AwayFromZero);
            _creditIN10D = Math.Round(_creditIN10USD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _wholeCashD = Math.Round(_wholeCashUSD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _wholeCreditD = Math.Round(_wholeCreditUSD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _wLGrossD = Math.Round(_wholeCashD - _landedCostD, 2, MidpointRounding.AwayFromZero);
            _wLMarginD = Math.Round(_wLGrossD / _wholeCashD * 100, 2, MidpointRounding.AwayFromZero);
            _retailD = Math.Round(_retailUSD * _exChangeRate, 2, MidpointRounding.AwayFromZero);
            _rtlGrossD = Math.Round((_retailD - _landedCostD) / _retailD * 100, 2, MidpointRounding.AwayFromZero);

            OnPropertyChanged();
            OnPropertyChanged(nameof(LandedCostUSD));
            OnPropertyChanged(nameof(LandedCostD));
            OnPropertyChanged(nameof(WholesaleUSD));
            OnPropertyChanged(nameof(WholesaleD));
            OnPropertyChanged(nameof(Sales5USD));
            OnPropertyChanged(nameof(Sales5D));
            OnPropertyChanged(nameof(CreditIN10USD));
            OnPropertyChanged(nameof(CreditIN10D));
            OnPropertyChanged(nameof(WholeCashUSD));
            OnPropertyChanged(nameof(WholeCashD));
            OnPropertyChanged(nameof(WholeCreditUSD));
            OnPropertyChanged(nameof(WholeCreditD));
            OnPropertyChanged(nameof(WLGrossUSD));
            OnPropertyChanged(nameof(WLGrossUSD));
            OnPropertyChanged(nameof(WLMarginUSD));
            OnPropertyChanged(nameof(WLMarginD));
            OnPropertyChanged(nameof(RetailUSD));
            OnPropertyChanged(nameof(RetailD));
            OnPropertyChanged(nameof(RTLGrossUSD));
            OnPropertyChanged(nameof(RTLGrossD));
        }
    }
}