﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week07.MnbServiceReference;

namespace week07
{
    public partial class Form1 : Form
    {  
        public Form1()
        {
            InitializeComponent();
            task03();
        }
        public string result;
        void task03()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);
            result = response.GetExchangeRatesResult;
        }
    }
}
