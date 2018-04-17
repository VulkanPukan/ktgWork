$(function() {

	var data1 = [
		["January", 10],
		["February", 8],
		["March", 4],
		["April", 13],
		["May", 17],
		["June", 9]
	];
	var data2 = [
		["January", 1],
		["February", 5],
		["March", 6],
		["April", 3],
		["May", 37],
		["June", 39]
	];
	 var data = [
            {
                label: "Registered",
                data: data1
            },
            {
                label: "Signedup",
                data: data2
            }
        ];
$.plot($("#placeholder1"), data, {
            series: {
                bars: {
                    show: true,
                    barWidth: 0.13,
                    order: 1
                }
            },
            valueLabels: {
                show: true
            }
        });
	// $.plot($("#multiBarChart"), [{
		// data: data1,
		// bars: {
			// show: true,
			// barWidth: 0.2,
			// align: "left",
		// }
	// }, {
		// data: data2,
		// bars: {
			// show: true,
			// barWidth: 0.2,
			// align: "right",
		// }
	// }], {
		// xaxis: {
			// mode: "categories",
			// tickLength: 0
		// }
	// });
});