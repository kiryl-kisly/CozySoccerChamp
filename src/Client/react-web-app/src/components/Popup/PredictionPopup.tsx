import './PredictionPopup.css'

interface Props {
	content: string
	isOpen: boolean
	onClose: () => void
}

export function PredictionPopup({ content, isOpen, onClose }: Props) {

	if (!isOpen) return null

	return (
		<div className='prediction-popup-overlay'>
			<div className='prediction-popup'>
				<button className='prediction-close-btn' onClick={onClose}>
					&times;
				</button>
				<div className='prediction-popup-content'>
					{content}
					content asdasdasdasd
					content asdasdasdasd
					content asdasdasdasd
					content asdasdasdasd
				</div>
			</div>
		</div>
	)
}