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
</p>

<table>
    <tr align="center">
        <td  style="background-color:#B7C68B">
            <div >
                <p style="font-family:arial;color:black;font-size:40px;text-align:center">{{user}}</p>
                <p>Current Date Range:</p>
                <p>From: {{dates[0]}}</p>
                <p>To: {{dates[1]}}</p>
                <p>worked Shifts: {{shiftCount}}</p>
            </div>
        </td>
        <td>
            <p>Shifts worked overview</p>
            <canvas id="monthlyShiftsBarChart" height="300" width="400"></canvas>
        </td>
    </tr>
    <tr align="center">
        <td>
            <table border = "1">
                <tr>
                    <td>Shift Date</td>
                    <td>Shift Start</td>
                    <td>Shift End</td>
                </tr>
                </table>
            <div style="overflow: auto;max-height: 400px;">
                <table border = "1">
                    %for shift in shiftList:
                    <tr>
                        <td>{{shift[3].split(' ')[0]}}</td>
                        <td>{{shift[3].split(' ')[1]}}</td>
                        <td>{{shift[4].split(' ')[1]}}</td>
                    </tr>
                    %end
                </table>
            </div>
        </td>
        <td>
            <p>Day/Night shift proportion</p>
            <canvas id="dayNightPieChart" height="300" width="300"></canvas>
        </td>
    </tr>
</table>

<script>
    var monthShiftData =
    {
        labels : ["January","February","March","April","May","June","July", "August", "September", "October", "November", "December"],
        datasets : [
            {
                fillColor : "rgba(255,170,51,0.5)",
                strokeColor : "rgba(220,220,220,1)",

                data : [{{dict[1]}},{{dict[2]}},{{dict[3]}},{{dict[4]}},{{dict[5]}},{{dict[6]}},{{dict[7]}},{{dict[8]}},{{dict[9]}},{{dict[10]}},{{dict[11]}},{{dict[12]}}]
            }]
    }
    var monthShiftOptions = { scaleOverride : true,
        scaleSteps : 20,
        scaleStartValue : 0,
        scaleStepWidth : 1,
        scaleShowLabels: true,
        scaleFontSize: 12
    }
    var dayNightPieData = [
        {
            value: {{dayNightRatio['Day']}},
            color: "#FF8C00"
        },
        {
            value: {{dayNightRatio['Night']}},
            color: "#3A3A38"
        }
    ]



    var myLine = new Chart(document.getElementById("monthlyShiftsBarChart").getContext("2d")).Bar(monthShiftData, monthShiftOptions);
    var dayNightPie = new Chart(document.getElementById("dayNightPieChart").getContext("2d")).Pie(dayNightPieData);

</script>



</body>
</html>


