// C3 Chart 1
var chart1 = c3.generate({
	bindto: '#lineGraph3',
	data: {
		columns: [
			['data1', 14, 28, 31, 49, 57, 59, 52, 48, 55, 58, 62, 60, 62, 58, 55, 61, 70, 80, 77, 78, 82, 98, 99, 121, 136, 115, 112, 120, 103, 117, 121, 126],
			['data2', 3, 16, 19, 24, 27, 32, 38, 36, 32, 36, 40, 48, 41, 44, 46, 53, 58, 62, 65, 61, 64, 62, 59, 63, 87, 92, 72, 81, 75, 80, 97, 97],
            ['data3', 7, 14, 16, 24, 28, 30, 30, 24, 27, 28, 31, 30, 31, 39, 27, 30, 35, 40, 37, 38, 41, 49, 49, 60, 70, 58, 58, 60, 51, 60, 60, 63],
		],
		names: {
			data1: 'Individual High',
			data2: 'Team Average',
			data3: 'Individual Low'
		},
		colors: {
			data1: '#f35454',
			data2: '#3a86c8',
            data2: '#F782AA'
		},
	}
});