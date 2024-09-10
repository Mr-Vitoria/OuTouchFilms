import { Component } from "react";

import style from "../../assets/css/shedule.module.scss";

import { getTodayShedule } from "../../api/animeService";

export class SheduleSection extends Component{
    constructor(props){
        super(props);

        this.state = {
            sheduleDay: this.props.sheduleDay ?? null
        }

        this.getTodaySheduleListEvent = this.getTodaySheduleListEvent.bind(this);
    }

    componentDidMount(){
        if(this.state.sheduleDay == null)
            this.getTodaySheduleListEvent();
    }

    async getTodaySheduleListEvent(){
        let result = await getTodayShedule();

        if(result != false){
            this.setState({
                sheduleDay: result
            });
        }
    }

    render(){
        return <section className={style.shedule}>
            <div className={style.header}>
                <h3>Сегодня выходят</h3>
                <a className="link" href="/shedule">Смотреть расписание на неделю</a>
            </div>
            <div className={style.body}>
                {
                    this.state.sheduleDay != null ?
                    this.state.sheduleDay.animeList.length <= 0 ?
                    <h2>Сегодня ничего не выходит</h2>
                    :
                    this.state.sheduleDay.animeList.map((anime, key) => {
                        return <a key={anime.id} href={`/detail?id=${anime.id}`} className={style.card}>
                        <img src={anime.poster} />
                        <div className={style.textContent}>
                            <p className={style.title}>{anime.name}</p>
                            <p className={style.seria}>Серия {anime.currentEpisodes + 1}</p>
                        </div>
                    </a>
                    })
                    : null
                }
            </div>
        </section>
    }
}