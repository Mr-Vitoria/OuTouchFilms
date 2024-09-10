import { Component } from "react";

import style from "../../../assets/css/detail.module.scss";

export class ReviewList extends Component {
    constructor(props) {
        super(props);
    }

    render() {
        return <section className={`${style.reviewSection}`}>
            <h3>Отзывы</h3>
            <div className={`${style.content}`}>
                {
                    this.props.reviews.length == 0 ?
                        <p>Еще нет ни 1 отзыва. Будьте первым</p>
                        : null
                }
                {
                    this.props.reviews.map((review, key) => {
                        return <div className={`${style.review}`} key={review.id}>
                            <div className={`${style.header}`}>
                                <img src={review.user.imgUrl} />
                                <p>{review.user.login}</p>
                            </div>
                            <p className={`${style.text}`}>{review.text}</p>
                            <div className={`${style.footer}`}>
                                <p>Недавно</p>
                            </div>
                        </div>
                    })
                }
            </div>
        </section>
    }
}