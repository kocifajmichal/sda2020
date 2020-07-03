using System.Reflection;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ambulance_api.Models;

namespace ambulance_api_tests
{
    [TestClass]
    public class AmbulanceExtensionTests
    {
        // Prvý pacient môže vstúpiť do ambulancie najskôr v čase otvorenia ambulancie.
        [TestMethod]
        public void PatientCannotEnterAmbulanceBeforeAmbulanceIsOpen()
        {
            // given
            var ambulance = CreateTestAmbulance();

            // when
            ambulance.EstimateAndSortWaitingList();

            // then
            Assert.IsTrue(ambulance.WaitingList[0].Estimated - DateTime.Today >= ambulance.OpeningTimeSpan);
        }

        // Pacient nemôže vstúpiť do ambulancie skôr ako prišiel.
        [TestMethod]
        public void PatientCannotEnterAmbulanceBeforeHeComes()
        {
            // given
            var ambulance = CreateTestAmbulance();
            ambulance.WaitingList[0].Since = new DateTime(2020, 7, 30, 7, 0, 0);
            ambulance.WaitingList[0].Estimated = new DateTime(2020, 7, 30, 6, 0, 0);

            // when
            ambulance.EstimateAndSortWaitingList();

            // then
            Assert.IsTrue(ambulance.WaitingList[0].Estimated >= ambulance.WaitingList[0].Since);
        }

        // Pacient môže vojsť do ambulancie až vtedy, keď boli vybavení všetci pacienti v poradí pred ním.
        [TestMethod]
        public void PatientCannotEnterBeforePreviousPatients()
        {
            // given (arrange)
            var ambulance = CreateTestAmbulance();
            ambulance.WaitingList.Add(new WaitingListEntry
            {
                Since = new DateTime(2020, 7, 3, 7, 40, 0),
                Estimated = new DateTime(2020, 7, 3, 0, 30, 0)
            });

            // when (act)
            ambulance.EstimateAndSortWaitingList();

            // then (assert)
            Assert.IsTrue(ambulance.WaitingList[1].Estimated >= 
                ambulance.WaitingList[0].Estimated + 
                TimeSpan.FromMinutes(ambulance.WaitingList[0].EstimatedDurationMinutes ?? 15));
        }

        [TestMethod]
        public void ShouldAcceptNullWaitingList()
        {
            // given
            var ambulance = new Ambulance();

            // when
            ambulance.EstimateAndSortWaitingList();

            // then
            Assert.IsNotNull(ambulance);
        }

        private Ambulance CreateTestAmbulance()
        {
            return new Ambulance
            {
                Name = "Doktor 1",
                OpeningTime = "08:00",
                WaitingList = new List<WaitingListEntry>
                {
                    new WaitingListEntry
                    {
                        Since = new DateTime(2020, 7, 3, 7, 30, 0),
                        Estimated = new DateTime(2020, 7, 3, 7, 30, 0),
                        EstimatedDurationMinutes = 20
                    }
                }
            };
        }
    }
}
