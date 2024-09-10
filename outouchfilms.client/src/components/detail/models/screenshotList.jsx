import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";

export class ScreenshotList extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return this.props.screenshots.length > 0 ?
            <section className={`${style.screenshootSection}`}>
                <h3>Кадры из аниме</h3>
                <div className={`${style.content}`}>
                    {
                        this.props.screenshots.map((screenshot, key) => {
                            return <img
                                key={`Screenshoot${key}`}
                                src={screenshot}
                                loading="lazy"
                                onClick={(ev) => {
                                    this.props.onClick(screenshot);
                                }}
                            />
                        })
                    }
                </div>
            </section>
            : null


    }
}