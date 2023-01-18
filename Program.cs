// See https://aka.ms/new-console-template for more information

using HtmlAgilityPack;
using Microsoft.VisualBasic;
using System;
using System.Formats.Asn1;
using System.Globalization;
using System.Runtime.CompilerServices;
using WebScrapper;
using CsvHelper;
using System.Net;
using System.Collections.Generic;
using System.IO;
using static System.Reflection.Metadata.BlobBuilder;


string url = "https://app.slack.com/client/T04BCBKBLLA/C04B5PSTB70";

//Console.WriteLine(doc.DocumentNode.InnerHtml);
//HtmlDocument doc = GetDocument(url);
HtmlDocument doc = GetDocument(url);
HtmlNodeCollection userInfo = doc.DocumentNode.SelectNodes("//*[@class=\"c-virtual_list__scroll_container\"]");

var data = new List<User>();

exportToCSV(data);

foreach (var user in userInfo)
{

    var userName = "//button[@class=\"c-link--button c-message__sender_button\"]";
    var userMessage = "//div[@class=\"p-rich_text_section\"]";
    var dateTxt = "//button[@class=\"c-button-unstyled c-message_list__day_divider__label__pill\"]";
    var info = new User
    {
        Name = user.SelectSingleNode(userName).InnerText,
        FindMessage = user.SelectSingleNode(userMessage).InnerText,
        Date = user.SelectSingleNode(dateTxt).InnerText,
    };
    data.Add(info);
}

//Convert to csv file
void exportToCSV(List<User> info) 
{
    using (var writer = new StreamWriter("./books.csv"))
    using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
    {
        csv.WriteRecords(info);
    }
}

//The funstion parses the URL return HTML document
static HtmlDocument GetDocument(string url)
{
    HtmlWeb web = new HtmlWeb();
    HtmlDocument doc = web.Load(url);
    return doc;
}

