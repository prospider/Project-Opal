import pygal
import os

bar_chart = pygal.Bar()
bar_chart.add('Fibonacci', [0,1,1,2,3,5,8,13,21,34,55])
#bar_chart.render_to_file(os.path.join('Resources','img','tempChart.svg'))
#bar_chart.render_to_png(os.path.join('Resources','img','tempChart.png'))