import React, { Component } from "react";
import style from "../../assets/css/index.module.scss";
import { CarouselItem } from "./models/CarouselItem";

export class AnimeCarousel extends Component {

    constructor(props) {
        super(props);

        this.state = {
            animeList: props.animeList,
            animeToViewList: [
                props.animeList[0],
                props.animeList[1],
                props.animeList[2],
                props.animeList[3],
                props.animeList[4],
                props.animeList[5],
                props.animeList[6]
            ],
            activeIndex: 3
        };

        this.carousel = React.createRef();
        this.textContainer = React.createRef();
        this.bgImage = React.createRef();
        this.changeBgEvent = null;

    }

    componentDidMount() {
        this.changeBgEvent = setTimeout(() => {
            this.changeCarouselItems(true);
        }, 8000);
    }

    render() {
        let activeAnime = this.state.animeList[this.state.activeIndex];
        if (activeAnime == undefined) {
            return <></>;
        }
        return <section className={`${style.topFilm}`}>
            <div className={`${style.bgImage}`} ref={this.bgImage} onAnimationEnd={(ev) => {
                if(ev.animationName.includes("disAppend")){
                    this.bgImage.current.classList.remove(style.disappend);
                    this.bgImage.current.classList.add(style.append);
                }
            }}>
                <img src={activeAnime.screenshots[0]} />
            </div>
            <div className={`${style.textContainer}`} ref={this.textContainer}>
                <p className={style.title}>{activeAnime.name}</p>
                <p className={style.info}>{activeAnime.year} | {activeAnime.maxEpisodes} серий | {activeAnime.status}</p>
                <p className={style.description}>{activeAnime.description}</p>
                <div className={style.btnContainer}>
                    <a className={`btn ${style.btn}`} href={`detail?id=${activeAnime.id}#player`}>
                        <img src="img/ico/play.svg" />Смотреть
                    </a>

                    <a className={`btn btnDark ${style.btn}`} href={`detail?id=${activeAnime.id}#trailerList`}>
                        Трейлер
                    </a>
                </div>
            </div>

            <div className={`${style.carousel}`}>
                <button className={`${style.control} ${style.prev}`} onClick={(ev) => {
                    this.changeCarouselItems(false);
                }}>
                    <img src="img/ico/arrow_right.svg" />
                </button>
                <div className={style.content}>
                    {this.state.animeToViewList.map((anime, key) => {

                        return <CarouselItem anime={anime} classCard={this.getClassAnimeCard(key)}
                            key={`Carousel${key}`}
                            animeIndex={this.state.animeList.findIndex(findAnime => findAnime.id == anime.id)} />
                    }
                    )}
                </div>
                <button className={`${style.control}`} onClick={(ev) => {
                    this.changeCarouselItems(true);
                }}>
                    <img src="img/ico/arrow_right.svg" />
                </button>
            </div>
            <div className={`${style.bottomBlur}`}></div>
        </section>
    }


    changeInformation(activeIndex) {
        this.bgImage.current.classList.remove(style.append);
        this.bgImage.current.classList.add(style.disappend);

        if (activeIndex >= this.state.animeList.length) {
            activeIndex = 0;
        }
        else if (activeIndex < 0) {
            activeIndex = this.state.animeList.length - 1;
        }
        this.setState({
            activeIndex: activeIndex
        });

    }

    changeCarouselItems(isNext) {

        if (this.changeBgEvent != null && this.changeBgEvent != undefined) {
            clearTimeout(this.changeBgEvent);
        }
        let newIndex = 0;
        
        if (isNext) {
            newIndex = this.state.activeIndex + 1;

            if (newIndex + 3 >= this.state.animeList.length) {
                this.state.animeToViewList.shift();
                this.state.animeToViewList.push(this.state.animeList[(newIndex + 3) - this.state.animeList.length]);
            }
            else {
                this.state.animeToViewList.shift();
                this.state.animeToViewList.push(this.state.animeList[newIndex + 3]);
            }
        }
        else {
            newIndex = this.state.activeIndex - 1;

            if (newIndex - 3 < 0) {
                this.state.animeToViewList.pop();
                this.state.animeToViewList.unshift(this.state.animeList[this.state.animeList.length + (newIndex - 3)]);
            }
            else {
                this.state.animeToViewList.pop();
                this.state.animeToViewList.unshift(this.state.animeList[newIndex - 3]);
            }
        }
        this.bgImage.current.classList.add(style.disappend);
        this.bgImage.current.classList.remove(style.append);

        this.changeInformation(newIndex);

        this.changeBgEvent = setTimeout(() => {
            this.changeCarouselItems(true);
        }, 8000);

    }

    getClassAnimeCard(i) {
        let classCard = '';
        switch (i) {
            case 0:
            case 6:
                classCard = style.outHtml;
                break;
            case 1:
            case 5:
                classCard = style.exitCard;
                break;
            case 2:
            case 4:
                classCard = style.nextCard;
                break;
            case 3:
                classCard = style.centerCard;
                break;

            default:
                break;
        }
        return classCard;
    }
}