<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8" />
    <title>Analytics</title>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.2/themes/smoothness/jquery-ui.css" />
    <script src="static/Chart.js"></script>
    <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script src="http://code.jquery.com/ui/1.10.2/jquery-ui.js"></script>
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