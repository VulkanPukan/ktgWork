var stateList = [,"Alabama", "Alaska", "Arizona",
                   "Arkansas", "California", "Colorado",
                   "Connecticut", "Delaware", "Florida",
                   "Georgia", "Hawaii", "Idaho",
                   "Illinois", "Indiana", "Iowa", "Kansas",
                   "Kentucky", "Louisiana", "Maine",
                   "Maryland", "Massachusetts", "Michigan",
                   "Minnesota", "Mississippi", "Missouri",
                   "Montana", "Nebraska", "Nevada", "New Hampshire",
                   "New Jersey", "New Mexico", "New York",
                   "North Carolina", "North Dakota", "Ohio",
                   "Oklahoma", "Oregon", "Pennsylvania","Rhode Island",
                   "South Carolina", "South Dakota", "Tennessee",
                   "Texas", "Utah", "Vermont",
                   "Virginia", "Washington", "West Virginia",
                   "Wisconsin", "Wyoming"];
function initSelectStateList(){
    var selectStateList = $('.state-select');
    for (var i = 0; i < selectStateList.length; i++) {
        var item = selectStateList[i];
        var fragment = document.createDocumentFragment();

        var opt = document.createElement('option');
        opt.innerHTML = "All";
        opt.value = 99;
        opt.id = "stateAllSelectOption";
        opt.style = "display:none";
        fragment.appendChild(opt);

        stateList.forEach(function (state, index) {
            var opt = document.createElement('option');
            opt.innerHTML = state;
            //opt.value = index + 1;
			opt.value = index;
            fragment.appendChild(opt);
        });

        item.appendChild(fragment);
    }
}

initSelectStateList();