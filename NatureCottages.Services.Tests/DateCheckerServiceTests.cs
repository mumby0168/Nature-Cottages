using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Text;
using NatureCottages.Services.Interfaces;
using NatureCottages.Services.Services;
using NUnit.Framework;

namespace NatureCottages.Services.Tests
{
    
    class DateCheckerServiceTests
    {
        private IDateCheckerService _dateCheckerService;

        [SetUp]
        public void Init()
        {
            _dateCheckerService = new DateCheckerService();
        }
        
        [Test]
        public void DateCheckerService_DoDatesIntercept_TrueWhenEndDateIntercepts()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = start1.AddDays(-3);
            var end2 = end1.AddDays(-2);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsTrue(res);
        }

        [Test]
        public void DateCheckerService_DoDatesIntercept_TrueWhenStartIntercepts()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = start1.AddDays(2);
            var end2 = end1.AddDays(5);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsTrue(res);
        }

        [Test]
        public void DateCheckerService_DoDatesIntercept_TrueWhenStartAndEndIntercepts()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = start1.AddDays(2);
            var end2 = end1.AddDays(-2);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsTrue(res);
        }

        [Test]
        public void DateCheckerService_DoDatesIntercept_FalseWhenStartAndEndBefore()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = DateTime.Now.AddDays(-7);
            var end2 = start2.AddDays(3);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsFalse(res);
        }


        [Test]
        public void DateCheckerService_DoDatesIntercept_FalseWhenStartAndEndAfter()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = start1.AddDays(8);
            var end2 = end1.AddDays(5);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsFalse(res);
        }


        [Test]
        public void DateCheckerService_DoDatesIntercept_TrueWhenStartAndEndCross()
        {

            var start1 = DateTime.Now;
            var end1 = start1.AddDays(7);

            var start2 = DateTime.Now.AddDays(-7);
            var end2 = end1.AddDays(3);

            //act
            var res = _dateCheckerService.DoDatesIntercept(start1, end1, start2, end2);

            Assert.IsTrue(res);
        }


    }
}
