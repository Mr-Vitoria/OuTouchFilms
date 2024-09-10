import { Component } from "react";
import { Header } from "./Header";
import { Footer } from "./Footer";

export class Layout extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <>
            <Header />
            <main>
                {this.props.children}
            </main>
            <Footer />
        </>
    }
}