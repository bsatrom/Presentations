using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Faceplant.Core.Models;
using Faceplant.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Faceplant.Tests.Mappings
{
    [TestClass]
    public class MappingTests
    {
        [TestInitialize]
        public void Init()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public void Speaker_should_map_to_SpeakerForm_with_valid_configuration()
        {
            Mapper.CreateMap<Session, string>().ConvertUsing(s => s.Name);
            Mapper.CreateMap<Speaker, SpeakerForm>();

            Mapper.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Speaker_should_map_to_SpeakerForm_with_valid_data()
        {
            var speaker = new Speaker
            {
                Email = "b@cv.d",
                Languages = new [] {"JavaScript", "C#"},
                Name = "Brandon Satrom",
                SpeakerId = 2343,
                State = "TX"
            };
            var session = new Session
            {
                Speaker = speaker,
                Date = DateTime.Now,
                Id = 123,
                Location = "Here",
                Name = "Automapper for Noobs",
                Ratings = new List<int> { 3, 5, 2, 1, 3 }
            };
            var sessions = new List<Session> {session, session};

            speaker.Sessions = sessions;

            Mapper.CreateMap<Session, string>().ConvertUsing(s => s.Name); 
            Mapper.CreateMap<Speaker, SpeakerForm>();            

            SpeakerForm speakerForm = Mapper.Map<Speaker, SpeakerForm>(speaker);

            Assert.IsNotNull(speakerForm);
            Assert.AreEqual("Brandon Satrom", speakerForm.Name);
            Assert.AreEqual(2, speakerForm.Sessions.Count());
        }   


        [TestMethod]
        public void SpeakerForm_should_map_to_Speaker_with_valid_configuration()
        {
            Mapper.CreateMap<SpeakerForm, Speaker>()
                .ForMember(s => s.Sessions, opt => opt.Ignore());

            Mapper.AssertConfigurationIsValid();
        }
    }
}
