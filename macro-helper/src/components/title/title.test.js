import React from 'react';
import renderer from 'react-test-renderer';
import { shallow } from 'enzyme';
import { Title } from './index';

describe('Title test suite', () => {

    let wrapper, component;

    beforeEach(() => {

        wrapper = shallow(<Title />);

        component = renderer.create(
            <Title />
        );
    });

    it('render without title', () => {

        const json = component.toJSON();

        expect(json).toMatchSnapshot();
    });

    it('render with title', () => {

        const title = 'cx-web-kisckstart!';

        component = renderer.create(
            <Title title={title} />
        );

        const json = component.toJSON();

        expect(json).toMatchSnapshot();
    });

});
