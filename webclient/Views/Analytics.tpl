<!doctype html>
<html xmlns="http://www.w3.org/1999/html">
<head>
    <title>Test Chart</title>
    <script src="static/Chart.js"></script>

</head>
<body>
<p>worked Shifts: {{shiftCount}}</p>
</br>
<canvas id="myChart" height="600" width="800"></canvas>

<script>
    var data = {
        labels : ["January","February","March","April","May","June","July", "August", "September", "October", "November", "December"],
        datasets : [
            {
                fillColor : "rgba(255,170,51,0.5)",
                strokeColor : "rgba(220,220,220,1)",

                data : [{{dict[1]}},{{dict[2]}},{{dict[3]}},{{dict[4]}},{{dict[5]}},{{dict[6]}},{{dict[7]}},{{dict[8]}},{{dict[9]}},{{dict[10]}},{{dict[11]}},{{dict[12]}}]
            }
        ]
    }
    var options = { scaleOverride : true,
                    scaleSteps : 20,
                    scaleStartValue : 0,
                    scaleStepWidth : 1,
                    scaleShowLabels: true,
                    scaleFontSize: 24}

    var myLine = new Chart(document.getElementById("myChart").getContext("2d")).Bar(data, options);
</script>
</body>
</html>


