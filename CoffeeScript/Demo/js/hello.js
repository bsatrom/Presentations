(function() {
  var sayHi;

  sayHi = function(name) {
    console.log("Hello ctxdnug!");
    return console.log("" + name + "!");
  };

  sayHi("Dale");

}).call(this);
