<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Analytics</title>
    <link rel="stylesheet" href="static/jquery-ui.min.css" />
    <script src="static/Chart.js"></script>
    <script src="static/jquery-1.9.1.js"></script>
    <script src="static/jquery-ui.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script>
        $(function() {
            $( ".dp" ).datepicker({
                numberOfMonths: 2,
                showButtonPanel : true,
                dateFormat :  "yy-mm-dd"
            });
        });

    </script>
</head>
<body>

<p>
<form action="/Analytics" method="POST">
    From:<input type="text" name="from"class="dp" id="from" />
    To:<input type="text" name="to" class="dp" id="to" />

    <select name='selecteduser'>
%for tuple in userList:
   <option value="{{tuple[1]}}">{{tuple[0]}}</option>
%end
        </select>
    <input type="submit" value="Analyze" />
</form>
</p><p>Please select a user.</p>
<form action='/Analytics' method='POST'>

    <input type='submit' value='Analyze' />

</body>
</html>