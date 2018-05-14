import { user } from 'firebase-functions/lib/providers/auth';

const functions = require('firebase-functions');
var firebase = require("@firebase/app").default;
require("firebase-auth");
require("@firebase/database");

// Leave out Storage
//require("firebase/storage");

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

const btnLogin = document.getElementById('btnLogin');
const btnLogout = document.getElementById('btnLogout');
const txtEmail = document.getElementById('txtEmail');
const txtPass = document.getElementById('txtPass');

// TODO: set timer in unity

btnLogin.addEventListener('click', e => {
  const email = txtEmail.value;
  const pass = txtPass.value;
  const auth = firebase.auth();
  const promise = auth.signInWithEmailAndPassword(email,pass);
  promise.catch(e => console.log(e.message));
});

