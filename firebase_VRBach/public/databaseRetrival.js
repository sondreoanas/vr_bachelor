// Initialize Firebase
var config = {
    apiKey: "AIzaSyCUJZDBvuEI0Snh2oJZ_l6mO5C4JRFOkn8",
    authDomain: "htcvruis2018.firebaseapp.com",
    databaseURL: "https://htcvruis2018.firebaseio.com",
    projectId: "htcvruis2018",
    storageBucket: "htcvruis2018.appspot.com",
    messagingSenderId: "311579352975"
};
firebase.initializeApp(config);

// referance to the specific database table
var dbSBRef = firebase.database().ref().child('scoreboard');
var dbCPRef = firebase.database().ref().child('curPlayer');

// function from firebase that is run everytime there is a change in the database
dbSBRef.on('value', snap => {
    var rawScoreBoard = snap.val();
    // using the values from the database takes time and have to happen inside this function in order to by compiled cronologically
    var scores = [];

    // adding the key value pair to an array entry by entry to be able to sort them
    for (key in rawScoreBoard) {
        scores.push({"key":key,"val":rawScoreBoard[key]});
    }

    // sorting function for key val pair based on val
    scores.sort(function(a, b){ return b.val - a.val; });

    // setting the TopScore
    document.getElementById("scoreTS").innerHTML = scores[0].val;
    document.getElementById("nameTS").innerHTML = scores[0].key;
    //removing top score from the array
    scores.splice(0,1);

    var HTMLReplacement = "";

    // setting up HTML for the rest of the scoreboard
    for (var index in scores) {
        HTMLReplacement += '<div class="scoreentry"><span class="name">' + scores[index].key + 
        '</span><span class="score">' + scores[index].val + '</span></div>'
    }
    document.getElementById("placeHolderSB").innerHTML = HTMLReplacement;
});


dbCPRef.on('value', snap => {
    var curPlayer = snap.val();
    var curPlayer_name
    var score = 0;
    var HTMLReplacementCS = "";

    if (curPlayer !== null) {
        for (key in curPlayer) {
            // database must have an entry to excist so there is a dummy element in the database
            if (key !== "dummy") {
                HTMLReplacementCS = '<div id="topscore"><p>Current Player:</p><p id="scoreTS">' + curPlayer[key] + 
                '</p><p id="nameTS">' + key + '</p></div>'
            }
        }
    }
    document.getElementById("placeHolderCP").innerHTML = HTMLReplacementCS;
});