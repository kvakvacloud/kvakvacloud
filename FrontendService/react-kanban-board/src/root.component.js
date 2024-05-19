import { useState, useSyncExternalStore } from "react";

export default function Root(props) {

const [boards, setBoards] = useState( [
  {id: 1, title: 'Сделать',items: [{id:1, title: 'Напиша нормальную доску'}, {id: 2, title: 'Переделай ее'}]},
  {id: 2, title: 'Проверить',items: [{id:3, title: 'Напиша нормальную доску'}, {id: 4, title: 'Переделай ее'}]},
  {id: 3, title: 'Готово',items: [{id:5, title: 'Напиша нормальную доску'}, {id: 6, title: 'Переделай ее'}]}
])

  return <section className="flex">
    { boards.map(board => 
    <ul className="board bg-slate-100 items-center mx-auto max-w-7xl rounded-md mt-10 p-10 px-10 min-w-80 min-h-48">
      <h2 className="board__title text-center text-3xl mb-6">{board.title}</h2>
      {board.items.map(item =>
        <li 
          draggable={true}
          className="item p-4"
          style={{ cursor: 'grab' }}>
          {item.title}
        </li>
      )}
    </ul>
    )}
  </section>;
}
