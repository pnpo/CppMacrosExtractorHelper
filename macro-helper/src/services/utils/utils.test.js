import utils from './utils';

describe('utils test suite', () => {

    let arr = [];

    expect.addSnapshotSerializer({
        test: (val) => {

            if (val != undefined) {
                
                return ;
            }
        },
        print: (val) => `${val.name}`
    });

    beforeEach(() => {

        arr = [{ name: 'lenon' }, { name: 'abramson' }, { name: 'zidan' }, { name: 'marley' }];    
    });

    it('sort string empty array', () => {

        const array = undefined;

        const result = utils.sort(array, 'name');

        expect(result).toMatchSnapshot();
    });

    it('sort string empty array - not sorted', () => {

        arr[1].name = undefined;

        const result = utils.sort([], 'name');

        expect(result).toMatchSnapshot();
    });

    it('sort string array without property name', () => {

        const result = utils.sort(arr);

        expect(result).toMatchSnapshot();
    });

    it('sort string array with undefined item', () => {

        arr[1].name = undefined;

        const result = utils.sort(arr, 'name');

        expect(result).toMatchSnapshot();
    });

    it('sort string array with empty item', () => {

        arr[1].name = '';

        const result = utils.sort(arr, 'name');

        expect(result).toMatchSnapshot();
    });

    it('sort string array with integer item', () => {

        arr[1].name = 3;

        const result = utils.sort(arr, 'name');

        expect(result).toMatchSnapshot();
    });
    
    it('sort string array defualt asc when order not defined', () => {

        const result = utils.sort(arr, 'name');

        expect(result).toMatchSnapshot();
    });

    it('sort string array asc', () => {

        const result = utils.sort(arr, 'name', 'asc');

        expect(result).toMatchSnapshot();
    });

    it('sort string array desc', () => {

        const result = utils.sort(arr, 'name', 'desc');

        expect(result).toMatchSnapshot();
    });

    it('sort string array asc case sensitive', () => {

        arr[0].name = 'LENON';
        
        const result = utils.sort(arr, 'name', 'asc');

        expect(result).toMatchSnapshot();
    });
});
