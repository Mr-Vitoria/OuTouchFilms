import React, { Component } from "react";

export class PageLoader extends Component {

    constructor(props) {
        super(props);

        this.close = this.close.bind(this);

        this.containerRef = React.createRef();
    }

    close() {
        this.containerRef.current.classList.remove("active");
    }

    render() {
        return <div ref={this.containerRef} className="parent active">
            <div id="load">
                <div>G</div>
                <div>N</div>
                <div>I</div>
                <div>D</div>
                <div>A</div>
                <div>O</div>
                <div>L</div>
            </div>
        </div>
    }
}