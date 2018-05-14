//import { user } from 'firebase-functions/lib/providers/auth';

const functions = require('firebase-functions');
var firebase = require("@firebase/app").default;
//require("firebase-auth");
require("@firebase/database");

var config = {
  apiKey: "AIzaSyCUJZDBvuEI0Snh2oJZ_l6mO5C4JRFOkn8",
  authDomain: "htcvruis2018.firebaseapp.com",
  databaseURL: "https://htcvruis2018.firebaseio.com",
  projectId: "htcvruis2018",
  storageBucket: "htcvruis2018.appspot.com",
  messagingSenderId: "311579352975"
};
firebase.initializeApp(config);

function writeUserScore(username, score) {
    firebase.database().ref('scoreboard/' + username).set(parseInt(score));
}

function cur_delete(username) {
  firebase.database().ref('curPlayer/' + username).set(null);
}

function edit_username(username,newUsername,score) {
  firebase.database().ref('scoreboard/' + newUsername).set(score);
  firebase.database().ref('scoreboard/' + username).set(null);
}

function cur_writeUserScore(username,score) {
  firebase.database().ref('curPlayer/'+ username).set(parseInt(score));
}

// Create and Deploy Your First Cloud Functions
// https://firebase.google.com/docs/functions/write-firebase-functions


exports.userentry = functions.https.onRequest((req, res) => {
  if (req.body.username !== null & req.body.score !== null) {
    writeUserScore(req.body.username,req.body.score);
    cur_delete(req.body.username);
    res.send("The score was succesfullly recived");
  } else {
    res.send(":(");
  }
});

exports.cur_userentry = functions.https.onRequest((req, res) => {
  if (req.body.username !== null & req.body.score !== null) {
    cur_writeUserScore(req.body.username,req.body.score);
    res.send("The score was succesfullly recived");
  } else {
    res.send(":(");
  }
});

exports.edit_userentry = functions.https.onRequest((req, res) => {
  if (req.body.username !== null && req.body.newUsername !== null && req.body.score !== null) {
    edit_username(req.body.username, req.body.newUsername, req.body.score);
    res.send("The entry was succesfully edited!");
  } else {
    res.send(":(")
  }
});
