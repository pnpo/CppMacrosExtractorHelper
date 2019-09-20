import sum from './sum';

describe('sum tests suite', () => {
  
    it('add without params retun NaN', () => {

        const result = sum();

        expect(result).toMatchSnapshot();
        //expect(result).toBe('NaN')
    });

    it('add with one params', () => {

        const result = sum(1);

        expect(result).toMatchSnapshot();
        //expect(result).toBe(NaN)
    });

    it('add with params', () => {

        const result = sum(1, 1);

        //expect(result).toBe(2)
        expect(result).toMatchSnapshot();
    });
});