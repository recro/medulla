---
---
(function() {
    function displaySearchResults(results, store) {
        var searchResults = document.getElementById('search-results');

        if (results.length) {
            var appendString = '';

            for (var i = 0; i < results.length; i++) {
                var item = store[results[i].ref];
                appendString += '<li class="mb-5">';
                appendString += '<a href="{{ "/" | absolute_url }}' + item.url + '" class="no-underline d-block py-1 border-bottom color-border-primary">';
                appendString += '<h2 class="h4">' + item.title + '</h2>';
                appendString += '</a>';
                appendString += '<p class="f4 mt-3">';
                appendString += item.intro;
                appendString += '</p>'
                appendString += '</li>';
            }

            searchResults.innerHTML = appendString;
        } else {
            searchResults.innerHTML = '<li>No results found</li>'
        }
    }

    function getQueryVariable(variable) {
        var query = window.location.search.substring(1);
        var vars = query.split('&');

        for (var i = 0; i < vars.length; i++) {
            var pair = vars[i].split("=");

            if (pair[0] === variable) {
                return decodeURIComponent(pair[1].replace(/\+/g, '%20'))
            }
        }
    }

    var searchTerm = getQueryVariable('query');

    if (searchTerm) {
        document.getElementById('search').setAttribute('value', searchTerm);
        document.getElementById('mobile-search').setAttribute('value', searchTerm);

        var idx = lunr(function() {
            this.field('id');
            this.field('title', { boost: 10 });
            this.field('shortTitle', { boost: 10 });
            this.field('intro', { boost: 5 });
            this.field('content', { boost: 5 });

            for (var key in window.store) {
                this.add({
                    'id': key,
                    'title': window.store[key].title,
                    'shortTitle': window.store[key].shortTitle,
                    'intro': window.store[key].intro,
                    'content': window.store[key].content
                });
            }
        });

        console.log(searchTerm);
        var results = idx.search(searchTerm);
        displaySearchResults(results, window.store);
    }
})();
