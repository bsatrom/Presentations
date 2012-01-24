using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Faceplant.Controllers;
using Faceplant.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;
using Should.Fluent;

namespace Faceplant.Tests.Controllers
{
    [TestClass]
    public class SessionsControllerTests
    {
        [TestMethod]
        public void create_get_should_provide_speakers_and_tags()
        {
            var sessionsController = SessionsControllerFactory();
            var createView = sessionsController.Create() as ViewResult;

            //Assert.IsNotNull(createView);
            createView.ShouldNotBeNull();

            var tags = createView.ViewBag.PossibleTags as IList<Tag>;
            var speakers = createView.ViewBag.PossibleSpeakers as IList<Speaker>;
            //Assert.IsNotNull(tags);
            //Assert.IsNotNull(speakers);
            tags.ShouldNotBeNull();
            speakers.ShouldNotBeNull();

            //Assert.IsTrue(tags.Count > 0);
            //Assert.IsTrue(speakers.Count > 0);
            tags.Count.ShouldBeInRange(1, 10);
            speakers.Count.ShouldBeInRange(2, 2);
        }

        [TestMethod]
        public void create_post_with_valid_session_should_redirect_to_index()
        {
            var sessionsController = SessionsControllerFactory();
            var session = CreateDummySession();
            
            var createPostView = sessionsController.Create(session) as RedirectToRouteResult;

            Assert.IsNotNull(createPostView);
            Assert.AreEqual("Index", createPostView.RouteValues["action"]);
        }

        [TestMethod]
        public void create_post_with_valid_session_should_create_session()
        {
            var sessionsController = SessionsControllerFactory();
            var session = CreateDummySession();
            sessionsController.Create(session);

            var detailsView = sessionsController.Details(session.Id) as ViewResult;
            var savedSession = detailsView.Model as Session;

            //Assert.IsNotNull(detailsView);
            //Assert.IsNotNull(savedSession);
            //Assert.AreEqual(session, savedSession);
            detailsView.Should().Not.Be.Null();
            savedSession.Should().Not.Be.Null();
            savedSession.Should().Equal(session);
            savedSession.Id.Should().Be.InRange(1, 5000);
            savedSession.Speaker.Name.Should().Equal("Brandon Satrom");
        }

        private static Session CreateDummySession()
        {
            return new Session
            {
                Name = "An Introduction to the Should Assertion Library",
                Date = DateTime.Now,
                Speaker = new Speaker { Name = "Brandon Satrom", SpeakerId = 123 },
                Location = "Austin, TX",
                Tag = new Tag() { TagId = 1, Name = "C#" }
            };
        }

        private SessionsController SessionsControllerFactory()
        {
            return new SessionsController(new FakeTagRepository(), new FakeSpeakerRepository(), new FakeSessionRepository());
        }
    }
}
