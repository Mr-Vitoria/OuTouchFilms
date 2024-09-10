export function CarouselItem(props) {
    return <a
        href={`/detail?id=${props.anime.id}`}
        animeid={props.animeIndex}
        className={props.classCard}
    >
        <img src={props.anime.poster} />
    </a>
}