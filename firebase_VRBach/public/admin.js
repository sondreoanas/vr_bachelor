var config = {
    apiKey: "AIzaSyCUJZDBvuEI0Snh2oJZ_l6mO5C4JRFOkn8",
    authDomain: "htcvruis2018.firebaseapp.com",
    databaseURL: "https://htcvruis2018.firebaseio.com",
    projectId: "htcvruis2018",
    storageBucket: "htcvruis2018.appspot.com",
    messagingSenderId: "311579352975"
};

firebase.initializeApp(config);

var dbSBRef = firebase.database().ref().child('scoreboard');

firebase.auth().onAuthStateChanged(user => {
    if (user) {
        console.log("succesful login!");
    } else {
        location.href = "./index.html"
    }
});

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

    var HTMLReplacement = "";

    // setting up HTML for the rest of the scoreboard
    for (var index in scores) {
        HTMLReplacement += '<div class="scoreentry">' +
        '<span id="' + scores[index].key + '" class="name">' + scores[index].key + '</span>' + 
        '<span id="' + scores[index].key + "score" + '"class="score">' + scores[index].val + '</span>' +
        '<button id="' + scores[index].key + 'edit' + '" name="' + scores[index].key + '" onclick="editFunction(this)" type="button">edit</button>' +
        '<button id="' + scores[index].key + "delete" + '"name="' + scores[index].key + '" onclick="deleteFunction(this)" type="button">delete</button>' + 
        '<button id="' + scores[index].key + 'done' + '" name="' + scores[index].key + '" onclick="editConfirmation(this)" style="display: none;">done</button></div>'
    }
    document.getElementById("placeHolderScoreboard").innerHTML = HTMLReplacement;
});

var button = document.getElementById("logout");

function deleteFunction (obj) {
    firebase.database().ref('scoreboard/' + obj.name).set(null);
}

function editFunction (obj) {
    var txtField = document.getElementById(obj.name);
    txtField.innerHTML = '<input type="text" placeholder="' + obj.name + '" id="' + obj.name + 'txtField' + '">';
    document.getElementById(obj.name + 'edit').style.display = 'none';
    document.getElementById(obj.name + 'delete').style.display = 'none';
    document.getElementById(obj.name + 'done').style.display = 'inline';
}

function editConfirmation (obj) {
    score = document.getElementById(obj.name + 'score').innerHTML;
    username = document.getElementById(obj.name + "txtField").value;
    console.log(username);
    console.log(obj.name);
    if (username != obj.name && username != "") {
        firebase.database().ref('scoreboard/' + username).set(score);
        firebase.database().ref('scoreboard/' + obj.name).set(null);
    } else {
        location.href = "./admin.html"
    }

}

button.onclick = function (){
    const auth = firebase.auth();
    firebase.auth().signOut();
    location.href = "./index.html";
};