var postalcodePattern = /[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d/;
var emailPattern = /^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$/;
var passwordPattern = /^(?=.*\d)(?=.*[A-Z]).{6,}$/;
var couldSumbit = true;

document.getElementById("clear").addEventListener("click", function () {
  document.getElementById("fname").value = "";
  document.getElementById("lname").value = "";
  document.getElementById("address").value = "";
  document.getElementById("city").value = "";
  document.getElementById("pcode").value = "";
  var provinceList = document.getElementsByName("province");
  for (let i = 0; i < provinceList.length; ++i) {
    if (provinceList[i].checked) {
      provinceList[i].checked = false;
    }
  }
  document.getElementById("age").value = "";
  document.getElementById("password").value = "";
  document.getElementById("confirm").value = "";
  document.getElementById("email").value = "";
  var couldSumbit = true;
  document.getElementById("codeError").innerHTML = "";
  document.getElementById("ageError").innerHTML = "";
  document.getElementById("emailError").innerHTML = "";
  document.getElementById("passwordError").innerHTML = "";
  document.getElementById("confirmError").innerHTML = "";
});
document.getElementById("register").addEventListener("click", function () {
  var fname = document.getElementById("fname").value;
  var lname = document.getElementById("lname").value;
  var address = document.getElementById("address").value;
  var city = document.getElementById("city").value;
  var pcode = document.getElementById("pcode").value;
  var provinceList = document.getElementsByName("province");
  var province = "";
  for (let i = 0; i < provinceList.length; ++i) {
    if (provinceList[i].checked) {
      province = provinceList[i].value;
    }
  }
  var age = document.getElementById("age").value;
  var password = document.getElementById("password").value;
  var confirm = document.getElementById("confirm").value;
  var email = document.getElementById("email").value;
  if (
    fname == "" ||
    lname == "" ||
    address == "" ||
    city == "" ||
    pcode == "" ||
    province == "" ||
    age == "" ||
    password == "" ||
    confirm == "" ||
    email == ""
  ) {
    alert("Please fill all the fields");
  } else {
    var code = pcode.replace(/ /g, "");
    if (code.length != 6 || !postalcodePattern.test(code)) {
      couldSumbit = false;
      document.getElementById("codeError").innerHTML =
        "The postal code has to be in the 'a0a0a0' format";
    } else {
      document.getElementById("codeError").innerHTML = "";
    }
    if (age * 1 < 18) {
      couldSumbit = false;
      document.getElementById("ageError").innerHTML =
        "Age has to be at least 18 years old";
    } else {
      document.getElementById("ageError").innerHTML = "";
    }
    if (!emailPattern.test(email)) {
      couldSumbit = false;
      document.getElementById("emailError").innerHTML =
        "The Email Field must contain the @ and . characters";
    } else {
      document.getElementById("emailError").innerHTML = "";
    }
    if (!passwordPattern.test(password)) {
      couldSumbit = false;
      document.getElementById("passwordError").innerHTML =
        "Passwords must have at least 6 characters and must contain at least one digit and one upper-case character";
    } else {
      document.getElementById("passwordError").innerHTML = "";
    }
    if (password != confirm) {
      couldSumbit = false;
      document.getElementById("confirmError").innerHTML =
        "The Confirm Password and Password should be same";
    } else {
      document.getElementById("confirmError").innerHTML = "";
    }
    if (couldSumbit) {
      alert("Registration successfully !");
    } else {
      alert("Wrong filled information");
      couldSumbit = true;
    }
  }
});
