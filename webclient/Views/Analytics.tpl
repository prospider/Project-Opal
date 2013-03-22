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
</p>

<p>worked Shifts: {{shiftCount}}</p>
</br>
<p>Shifts worked Every Month</p>
</br>
<canvas id="barChart" height="300" width="400"></canvas>

<canvas id="shiftLength"></canvas>

<script>
    var data =
    {
        labels : ["January","February","March","April","May","June","July", "August", "September", "October", "November", "December"],
        datasets : [
            {
                fillColor : "rgba(255,170,51,0.5)",
                strokeColor : "rgba(220,220,220,1)",

                data : [{{dict[1]}},{{dict[2]}},{{dict[3]}},{{dict[4]}},{{dict[5]}},{{dict[6]}},{{dict[7]}},{{dict[8]}},{{dict[9]}},{{dict[10]}},{{dict[11]}},{{dict[12]}}]
            }]
    }
    var options = { scaleOverride : true,
        scaleSteps : 20,
        scaleStartValue : 0,
        scaleStepWidth : 1,
        scaleShowLabels: true,
        scaleFontSize: 12}

    var myLine = new Chart(document.getElementById("barChart").getContext("2d")).Bar(data, options);
</script>



</body>
</html>


