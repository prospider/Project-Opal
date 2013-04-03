<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="http://fonts.googleapis.com/css?family=Crimson+Text" rel="stylesheet" type="text/css">
    <link href="http://fonts.googleapis.com/css?family=Allerta" rel="stylesheet" type="text/css">
</head>

<body>
<center>
<h1>Register New Employee</h1>
<form method="POST" action="/Registering">
    First Name: <input name="First Name"     type="text" />
    </br>
    Last Name: <input name="Last Name"    type="text" />
    </br>
    Address: <input name="Address"    type="text" />
    </br>
    Wage: <input name="Wage"    type="number" step="any" min="10.25"/>
    </br>
    SIN: <input name="SIN"    type="text" />
    </br>
    Password: <input name="Password"  type="password" />
    </br>
    Confirm Password:<input name="Confirm Password" type="password">
    </br>
    <input type="submit" />
</form>

<style type="text/css">
input {
	padding: 5px;
	font-size: 15px;
	text-shadow: 0px 1px 0px #fff;
	outline: none;
	background: -webkit-gradient(linear, left top, left bottom, from(#bcbcbe), to(#ffffff));
	background: -moz-linear-gradient(top,  #bcbcbe,  #ffffff);
	-webkit-border-radius: 3px;
	-moz-border-radius: 3px;
	border-radius: 3px;
	border: 1px solid #717171;
	-webkit-box-shadow: 1px 1px 0px #efefef;
	-moz-box-shadow: 1px 1px 0px #efefef;
	box-shadow:  1px 1px 0px #efefef;
}
h1 {
    font-family: 'Allerta', Helvetica, Arial, sans-serif;
    font-size: 50px;
    line-height: 55px;
}
label {
    font-family: 'Crimson Text', Georgia, Times, serif;
    font-size: 16px;
    line-height: 25px;
}
</style>
</center>
</body>
</html>