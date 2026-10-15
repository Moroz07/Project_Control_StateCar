from enum import Enum


class RepairStatus(str, Enum):
    NEW = "open"
    IN_PROGRESS = "in_progress"
    COMPLETED = "closed"
    CANCELLED = "cancelled"