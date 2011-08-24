/// <reference path="../jquery-1.6.1-vsdoc.js" />
/// <reference path="../jQuery.tmpl.js" />
/// <reference path="../knockout-1.2.1.debug.js" />

var viewModel = {
    id: '',
    name: ko.observable(''),
    email: ko.observable(''),
    bio: ko.observable(''),
    twitterHandle: ko.observable(''),
    state: ko.observable(''),
    photoUrl: ko.observable(''),
    languages: ko.observableArray([]),
    favoriteTopics: ko.observableArray([]),
    averageRating: ko.observable(''),
    languageToAdd: ko.observable(''),
    topicToAdd: ko.observable('')
};
viewModel.enableAddLanguage = function () {
    return languageToAdd().length > 0;
}
viewModel.twitterUrl = ko.dependentObservable(function () {
    return "http://www.twitter.com/" + viewModel.twitterHandle();
});
viewModel.addLanguage = function () {
    if (viewModel.languageToAdd() !== '') {
        viewModel.languages.push(viewModel.languageToAdd());
        viewModel.languageToAdd('');
    }
};
viewModel.addTopic = function () {
    if (viewModel.topicToAdd() !== '') {
        viewModel.favoriteTopics.push(viewModel.topicToAdd());
        viewModel.topicToAdd('');
    }
};

ko.applyBindings(viewModel);

$('#profilePreview').hide();
$('input[type=text]').blur(function () {
    $('#profilePreview').show();
});
$('#profilePreview').draggable();