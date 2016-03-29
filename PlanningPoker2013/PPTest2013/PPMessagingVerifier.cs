using System;
using System.Security.Cryptography;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PPMessagingMediatR;
using PPObjects;

namespace PPTest2013
{

    public partial class PPMessagingVerifier
    {
        static internal string participant1 = "Part1";
        static internal string participant2 = "Part2";
        static internal TableData CreateTable()
        {
            var td =new TableFactory().CreateTable("Moderator");
            return td;

        }
      
    }
}
