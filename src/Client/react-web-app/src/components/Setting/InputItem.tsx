import { useState } from "react"

interface Props {
	initialValue: string
	onChange: (value: string) => void
	onValidChange: (isValid: boolean) => void
}

export function InputItem({ initialValue, onChange, onValidChange }: Props) {
	const [input, setInput] = useState(initialValue)
	const [error, setError] = useState("")

	const validate = (value: string) => {
		if (!/^[a-zA-Z0-9_]*$/.test(value)) {
			setError("Only a-z, A-Z, 0-9 and underscores allowed")
			onValidChange(false)
			return false
		}

		if (value.length < 5) {
			setError("Must be at least 5 characters")
			onValidChange(false)
			return false
		}

		setError("")
		onValidChange(true)
		return true
	}

	const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
		const value = e.target.value
		setInput(value)
		onChange(value)
		validate(value)
	}

	return (
		<div className="flex flex-col px-4 py-2 relative">
			<input
				type="text"
				value={input}
				onChange={handleChange}
				className="text-white font-medium text-lg bg-transparent outline-none px-2 py-1 w-full"
				placeholder="Enter username..."
			/>
			{error && <p className="text-red-500 text-sm mt-1">{error}</p>}
		</div>
	)
}
