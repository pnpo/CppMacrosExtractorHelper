import React, { Component } from 'react';
import { SideMenu, ContentWrapper, Panel, Button } from '@checkmarx/cx-ui';
import { Title } from '../title';
import Card from '../card';

import binary from '../../assets/images/png/icons-cpp/binary.png';
import hardware from '../../assets/images/png/icons-cpp/hardware.png';
import typing from '../../assets/images/png/icons-cpp/typing.png';

import '../../styles/main.scss';
import Manually from '../formPages/Manually';
import LibraryImport from '../formPages/libraryImport';
import ProjectImport from '../formPages/projectImport';
import Help from '../formPages/helpPage';

export default class MainPortalWrapper extends Component {
    constructor(props) {
        super(props);

        this.state ={
            option:0
        };
      }

    changePage(val){
        this.setState({option: val});
    }

    render() {

        const mainWrapperStyle = {
            marginLeft: '90px',
            paddingRight: '30px'
        };
        const {option} = this.state;
        return(
            <div style={mainWrapperStyle}>
                <SideMenu />
                <Title
                    title='C++ Macro Helper'
                />
                <ContentWrapper>
                    <Panel>
                        <div style={{height: '83vh', padding: 25}}>
                        
                        <div>
                                    <div>   
                                    <Card action={() => this.changePage(1)} image={binary} text={'Insert Manually'}/> 
                                    </div>
                                    <div>      
                                    <Card action={() => this.changePage(2)} image={hardware} text={'Load Library'}/>
                                    </div>
                                <div>   
                                    <Card action={() => this.changePage(3)} image={typing} text={'Import Project'}/>
                                </div>
                                <div>   
                                    <Button onClick={() => this.changePage(0)} text={'Help'}></Button>
                                </div>
                        </div>
                        </div>
                    </Panel>
                    <Panel>
                        <div style={{padding: 25}}>
                        {option == 0 && 
                            <Help/>
                        }
                        {option == 1 && 
                            <Manually/>
                        }
                        {option == 2 && 
                            <LibraryImport/>
                        }
                        {option == 3 && 
                            <ProjectImport/>
                        }
                        </div>
                    </Panel>
                </ContentWrapper>
            </div>
        );
    }
}