using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using System.Configuration;

public class FlashcardsService
{
    private string? connectionString;
    public FlashcardsService()
    {
        connectionString = ConfigurationManager.AppSettings.Get("connectionString");

    }
}