// Data that would typically come from the server
var availableMeals = [
    { mealName: "Standard (sandwich)", price: 0 },
    { mealName: "Premium (lobster)", price: 34.95 },
    { mealName: "Ultimate (leather shoe)", price: 290 }
];

// Object to manage seat reservations
var seatReservation = function(name) {
    this.name = name;
    this.availableMeals = availableMeals;
    this.meal = ko.observable(availableMeals[0]);
}

// ViewModel with state
var viewModel = {
    seats: ko.observableArray([
        new seatReservation("Steve"),
        new seatReservation("Bert")
    ]), 
    addSeat: function() {
        this.seats.push(new seatReservation());   
    }
};

// Again, just Knockout doing its thing
ko.applyBindings(viewModel);