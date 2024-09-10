import { Component } from 'react';
import { Layout } from './components/shared/Layout';
import Routing from './components/shared/Routing';

export default class App extends Component {
    constructor(props) {
        super(props);

    }

    render() {
        return <Layout>
                   <Routing />
               </Layout>
    }
}