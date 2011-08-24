/// <reference path="../jquery-1.6.1-vsdoc.js" />
/// <reference path="../jQuery.tmpl.js" />
/// <reference path="../knockout-1.2.1.debug.js" />

var speakerForm = window.speakerForm || {};

var viewModel = {
    speakerId: speakerForm.id,
    name: ko.observable(speakerForm.name),
    email: ko.observable(speakerForm.email),
    bio: ko.observable(speakerForm.bio),
    twitterHandle: ko.observable(speakerForm.twitterHandle),
    state: ko.observable(speakerForm.state),
    photoUrl: ko.observable(speakerForm.photoUrl),
    languages: ko.observableArray([]),
    favoriteTopics: ko.observableArray([]),
    sessions: speakerForm.sessions,
    averageRating: ko.observable(speakerForm.averageRating),
    languageToAdd: ko.observable(''),
    topicToAdd: ko.observable('')
};
viewModel.twitterUrl = ko.dependentObservable(function () {
    return "http://www.twitter.com/" + viewModel.twitterHandle();
});
viewModel.addLanguage = function () {
    if (viewModel.languageToAdd() != '') {
        viewModel.languages.push(viewModel.languageToAdd());
        viewModel.languageToAdd('');
    }
};
viewModel.addTopic = function () {
    if (viewModel.topicToAdd() != '') {
        viewModel.favoriteTopics.push(viewModel.topicToAdd());
        viewModel.topicToAdd('');
    }
};
viewModel.addSpeaker = function () {
    $.ajax({
        url: "/speakers/create/",
        type: 'post',
        data: ko.toJSON(this),
        contentType: 'application/json',
        success: function (result) { alert(result); }
    });
};
ko.applyBindings(viewModel);

$('#profilePreview').draggable();