<!DOCTYPE html>
<html>
<head>
    <title></title>
    <link href="http://fonts.googleapis.com/css?family=Crimson+Text" rel="stylesheet" type="text/css">
    <link href="http://fonts.googleapis.com/css?family=Allerta" rel="stylesheet" type="text/css">
</head>
<body>
<center>
<h1>Diamond Taxi</h1>
    <form method="POST" action="/login">

        <div class="ctrlHolder">
          <label for="">        Name</label>
          <input type="text" id="" name="name" value="" size="35" class="textInput">
        </div>

        <div class="ctrlHolder">
          <label for="">Password</label>
          <input type="password" id="" name="password" value="" size="35" class="textInput">
        </div>

      <div class="buttonHolder"><button type="submit" class="primaryAction">Submit</button></div>
    </form>

<style type="text/css">
.primaryAction {
	background:#25A6E1;
	background:-moz-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
	background:-webkit-gradient(linear,left top,left bottom,color-stop(0%,#25A6E1),color-stop(100%,#188BC0));
	background:-webkit-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
	background:-o-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
	background:-ms-linear-gradient(top,#25A6E1 0%,#188BC0 100%);
	background:linear-gradient(top,#25A6E1 0%,#188BC0 100%);
	filter:progid:DXImageTransform.Microsoft.gradient(startColorstr='#25A6E1',endColorstr='#188BC0',GradientType=0);
	padding:8px 13px;
	color:#fff;
	font-family:'Helvetica Neue',sans-serif;
	font-size:17px;
	border-radius:4px;
	-moz-border-radius:4px;
	-webkit-border-radius:4px;
	border:1px solid #1A87B9
}

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