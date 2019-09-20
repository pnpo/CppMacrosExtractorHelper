import _ from 'lodash';

class Utils {

    sort = (arr, name, order) => {

        order = order ? order : 'asc';

        return _.orderBy(arr, [s => {

            if (typeof s[name] == 'string') {
            
                return s[name].toLowerCase();
            }

            return s[name];            
            
        }], [order]);
    }
}

export default new Utils();