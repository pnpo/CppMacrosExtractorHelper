import React from 'react';
import Button from './index';
import { mount, shallow } from 'enzyme';
import toJson from 'enzyme-to-json';

describe('button tests suite', () => {

    it('render correctly', () => {

        expect(shallow(<Button />)).toMatchSnapshot();
    });

    it('render correctly', () => {

        expect(shallow(<Button />)).toMatchSnapshot();
    });

    it('render with text', () => {

        const buttonText = 'blue button';

        const wrapper = shallow(<Button text={buttonText} />);

        console.log(wrapper.debug());

        expect(wrapper.find('.rootButton').text()).toBe(buttonText);
    });

    it('render disabled mode', () => {

        const buttonText = 'blue button';

        const wrapper = shallow(<Button text={buttonText} disabled />);

        console.log(wrapper.html());

        expect(wrapper.hasClass('disabled')).toBeTruthy();
    });

    it('click button', () => {

        const buttonText = 'blue button';

        const mock = jest.fn();

        const wrapper = shallow(<Button 
                text={buttonText}
                onClick={mock} />);

        console.log(wrapper.html());

        wrapper.find('button').simulate('click');
        wrapper.find('button').simulate('click');
        wrapper.find('button').simulate('click');

        expect(mock).toBeCalled();
        expect(mock).toHaveBeenCalledTimes(3);
    });
});