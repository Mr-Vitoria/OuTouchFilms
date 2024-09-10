import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";

export class TrailerList extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.trailers.length > 0 ?
            <section className={`${style.trailerSection}`} id="trailerList">
                <h3>Трейлеры</h3>
                <div className={`${style.content}`}>
                    {
                        this.props.trailers.map((trailer, key) => {
                            return <iframe key={`Trailer${key}`} src={trailer.replace('http','https')} loading="lazy" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" allowFullScreen={true}></iframe>
                        })
                    }
                </div>
            </section>
            : null
    }
}