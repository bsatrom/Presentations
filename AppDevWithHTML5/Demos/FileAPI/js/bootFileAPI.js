// Include this script to bootstrap Microsoft HTLM5 File API - Experimental Release

function AddTestOnlyWarning() {
    var message = '<div style="height: 39px; background-color:Red; color:White; "><p style="font-size:18px; top: 20px;">'+
              '<b>This page is using Microsoft File API prototype code. The code is for testing-purposes only!</b></p>'+
              '</div>';

    var bodyEl = document.getElementsByTagName("body");
    if (bodyEl.length == 0) {
        var newEl = document.createElement("body");
    } 
    else {
        var newEl = document.createElement("div");
    }
    newEl.innerHTML = message;
    document.getElementsByTagName("html")[0].appendChild(newEl);
}

AddTestOnlyWarning();

window.URLDraft = GetFileURL();

window.FileReaderDraft = function () {
    this.reader = new ActiveXObject("FileAPI.FileReader");
    if (window.JSON) {
        this.reader.json = window.JSON;
    }
    else {
        var jsonObject = {
             parse: function (txt) {
                 if (txt === "[]") return [];
                 if (txt === "{}") return {};
                 throw { message: "Unrecognized JSON to parse: " + txt };
             }
        }
        this.reader.json = jsonObject;
    }

    thisReader = this;

    this.reader.onloadstart = function(event) {
        if (thisReader.onloadstart) {
            thisReader.onloadstart(event);
        }
    };
    this.reader.onprogress = function(event) {
        if (thisReader.onprogress) {
            thisReader.onprogress(event);
        }
    };
    this.reader.onload = function(event) {
        if (thisReader.onload) {
            thisReader.onload(event);
        }
    };
    this.reader.onabort = function(event) {
        if (thisReader.onabort) {
            thisReader.onabort(event);
        }
    };
    this.reader.onerror = function(event) {
        if (thisReader.onerror) {
            thisReader.onerror(event);
        }
    };
    this.reader.onloadend = function(event) {
        if (thisReader.onloadend) {
            thisReader.onloadend(event);
        }
    };
};

window.FileReaderDraft.prototype.close = function () {
    this.reader.close();
};

window.FileReaderDraft.prototype.abort = function () {
    this.reader.abort();
};

window.FileReaderDraft.prototype.readAsText = function (file) {
    this.reader.readAsText(file);
};

function GetFileSelector() {
    var fileSelector = new ActiveXObject("FileAPI.FileSelector");
    if (window.JSON) {
        fileSelector.json = window.JSON;
    }
    else {
        var jsonObject = {
             parse: function (txt) {
                 if (txt === "[]") return [];
                 if (txt === "{}") return {};
                 throw { message: "Unrecognized JSON to parse: " + txt };
             }
         }
         fileSelector.json = jsonObject;
    }

    return fileSelector;
}

function GetFileURL() {
    var fileURL = new ActiveXObject("FileAPI.FileURL");
    if (window.JSON) {
        fileURL.json = window.JSON;
    }
    else {
        var jsonObject =
        {
            parse: function (txt) {
                if (txt === "[]") return [];
                if (txt === "{}") return {};
                throw { message: "Unrecognized JSON to parse: " + txt };
            }
        }
        fileURL.json = jsonObject;
    }

    return fileURL;
}